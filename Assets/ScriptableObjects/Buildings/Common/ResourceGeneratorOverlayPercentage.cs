using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlayPercentage : MonoBehaviour {

    BuildingSO buildingType;
    BuildingTypeHolder buildingTypeHolder;

    SpriteRenderer icon;
    TextMeshPro text;

    private void Start() {
        icon = transform.Find("icon").GetComponent<SpriteRenderer>();
        UpdateSprite();
        text = transform.Find("text").GetComponent<TextMeshPro>();

        buildingTypeHolder = transform.parent.GetComponent<BuildingTypeHolder>();
        buildingTypeHolder.OnBuildingTypeChanged += OnBuildingTypeChanged;
    }

    private void OnBuildingTypeChanged(object sender, System.EventArgs e) {
        buildingType = buildingTypeHolder.BuildingType;
        UpdateSprite();
    }

    private void UpdateSprite() {
        if(buildingType != null) {
            icon.sprite = buildingType.resourceGeneratorConfig.ResourceType.Sprite;
        }
    }

    private void Update() {
        if (buildingType != null) {
            var resourceNodeCount = ResourceNodesSearch.NearbyNodes(
                transform.parent.position, buildingType.resourceGeneratorConfig
            );
            var percentage = (float) resourceNodeCount / buildingType.resourceGeneratorConfig.maxResourceNodes * 100f;

            if(percentage == 100f){
                text.text = percentage.ToString() + "%";
            }

            text.text = percentage.ToString("0.#") + "%";
        }
    }

}
