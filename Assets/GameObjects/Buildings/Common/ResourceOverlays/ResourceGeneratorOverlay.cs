using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour {

    public ResourceGenerator resourceGenerator;
    private TextMeshPro text;

    void Start() {
        resourceGenerator = transform.parent.GetComponent<ResourceGenerator>();

        var icon = transform.Find("icon").GetComponent<SpriteRenderer>();
        icon.sprite = resourceGenerator.Building.resourceGeneratorConfig.ResourceType.Sprite;

        text = transform.Find("text").GetComponent<TextMeshPro>();
        text.text = resourceGenerator.ResourcesPerSecond.ToString("0.0");
        resourceGenerator.OnResourcesPerSecondChange += ResourceGeneratorOnResourcesPerSecondChange;
    }

    private void ResourceGeneratorOnResourcesPerSecondChange(object sender, EventArgs e) {
        text.text = resourceGenerator.ResourcesPerSecond.ToString("0.0");
    }

    void Update() {
        var timerNormalized = resourceGenerator.timer / resourceGenerator.timerMaxInSeconds;
        transform.Find("bar").localScale = new Vector3(timerNormalized, 1, 1);
    }
}
