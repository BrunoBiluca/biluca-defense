using TMPro;
using UnityEngine;

namespace Assets.GameObjects.Buildings {
    public class ResourceGeneratorOverlayPercentage : MonoBehaviour {

        BuildingSO buildingType;
        BuildingTypeHolder buildingTypeHolder;

        SpriteRenderer icon;
        TextMeshPro text;

        private void Awake() {
            icon = transform.Find("icon").GetComponent<SpriteRenderer>();
            text = transform.Find("text").GetComponent<TextMeshPro>();
        }

        private void Start() {
            buildingTypeHolder = transform.parent.GetComponent<BuildingTypeHolder>();
            buildingTypeHolder.OnBuildingTypeChanged += OnBuildingTypeChanged;
        }

        private void OnBuildingTypeChanged(object sender, System.EventArgs e) {
            buildingType = buildingTypeHolder.BuildingType;

            if(buildingType == null) return;

            if(buildingType.itGenerateResources) {
                gameObject.SetActive(true);
                UpdateSprite();
            } else {
                gameObject.SetActive(false);
            }
        }

        private void UpdateSprite() {
            icon.sprite = buildingType.resourceGeneratorConfig.ResourceType.Sprite;
        }

        private void Update() {
            if(buildingType != null && buildingType.itGenerateResources) {
                var resourceNodeCount = ResourceNodesSearch.NearbyNodes(
                    transform.parent.position, buildingType.resourceGeneratorConfig
                );
                var percentage = (float)resourceNodeCount / buildingType.resourceGeneratorConfig.maxResourceNodes * 100f;

                if(percentage == 100f) {
                    text.text = percentage.ToString() + "%";
                }

                text.text = percentage.ToString("0.#") + "%";
            }
        }

    }
}