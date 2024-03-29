using Assets.GameManagers;
using Assets.UnityFoundation.DebugHelper;
using System;
using UnityEngine;

namespace Assets.GameObjects.Buildings {
    public class BuildingTypeHolder : MonoBehaviour {

        public EventHandler OnBuildingTypeChanged;

        [SerializeField]
        private BuildingSO buildingType;
        public BuildingSO BuildingType {
            get { return buildingType; }
            set {
                buildingType = value;
                OnBuildingTypeChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Update() {
            if(GameManager.Instance.DebugMode) {
                DebugBuildingType();
            }
        }

        private void DebugBuildingType() {
            if(buildingType == null) return;

            var buildingCollider = buildingType.Prefab.GetComponent<BoxCollider2D>();
            DebugDraw.DrawRectangle(
                transform.position + (Vector3)buildingCollider.offset,
                buildingCollider.size,
                Color.red
            );
            DebugDraw.DrawCircle(
                transform.position,
                buildingType.resourceGeneratorConfig.detectionRadius,
                Color.blue
            );
        }
    }

}
