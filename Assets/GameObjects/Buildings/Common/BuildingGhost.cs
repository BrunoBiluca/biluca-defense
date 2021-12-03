using Assets.GameManagers;
using Assets.UnityFoundation.CameraScripts;
using UnityEngine;

namespace Assets.GameObjects.Buildings {
    public class BuildingGhost : MonoBehaviour {

        public GameObject GhostSprite { get; set; }

        public BuildingTypeHolder holder;

        void Awake() {
            GhostSprite = transform.Find("sprite").gameObject;
        }

        void Start() {
            BuildingManager.Instance.OnCurrentBuildingChanged += BuildingManager_OnCurrentBuildingChanged;
            Toggle(BuildingManager.Instance.CurrentBuilding);
        }

        private void BuildingManager_OnCurrentBuildingChanged(
            object sender, BuildingManager.OnCurrentBuildingChangedEventArgs e
        ) {
            Toggle(e.CurrentBuilding);
        }

        private void Toggle(BuildingSO currentBuilding) {
            holder.BuildingType = currentBuilding;
            if(currentBuilding == null) {
                Hide();
            } else {
                Show(currentBuilding.Sprite);
            }
        }

        private void Hide() {
            gameObject.SetActive(false);
        }

        private void Show(Sprite sprite) {
            gameObject.SetActive(true);
            GhostSprite.GetComponent<SpriteRenderer>().sprite = sprite;
        }


        void Update() {
            transform.position = CameraUtils.GetMousePosition2D();
        }
    }
}