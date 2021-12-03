using Assets.GameManagers;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour {

    private ResourceListSO availableResources;
    private Dictionary<ResourceTypeSO, Transform> resources;

    void Awake() {
        availableResources = Resources.Load<ResourceListSO>("available_resources_lvl_0");
        resources = new Dictionary<ResourceTypeSO, Transform>();
        CreateResourceUI();
    }

    void Start() {
        UpdateResourceUI();
        ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
    }

    private void UpdateResourceUI() {
        foreach(var resource in availableResources.Resources) {
            var amount = ResourceManager.Instance.GetResourceAmount(resource);

            resources[resource]
                .Find("text")
                .GetComponent<TextMeshProUGUI>()
                .SetText(amount.ToString());
        }
    }

    private void ResourceManager_OnResourceAmountChanged(object sender, EventArgs e) {
        UpdateResourceUI();
    }

    private void CreateResourceUI() {
        var resourceTemplate = transform.Find("resourceTypeTemplate");
        resourceTemplate.gameObject.SetActive(false);
        var baseOffset = resourceTemplate.GetComponent<RectTransform>().anchoredPosition.x;

        var index = 0;
        foreach(var resource in availableResources.Resources) {
            var resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);

            float offeset = -150f;
            resourceTransform
                .GetComponent<RectTransform>()
                .anchoredPosition = new Vector2(baseOffset + offeset * index++, 0);

            var imageTransform = resourceTransform.Find("image");
            imageTransform.GetComponent<Image>().sprite = resource.Sprite;

            resources.Add(resource, resourceTransform);
        }

    }

}
