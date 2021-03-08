using Assets.GameObjects.Buildings;
using Assets.UI;
using Assets.UnityFoundation.CameraUtils;
using System;
using UnityEngine;

namespace Assets.GameManagers {
    public class BuildingManager : MonoBehaviour {

        public static BuildingManager Instance { get; private set; }

        public event EventHandler<OnCurrentBuildingChangedEventArgs> OnCurrentBuildingChanged;
        public class OnCurrentBuildingChangedEventArgs : EventArgs {
            public BuildingSO CurrentBuilding { get; set; }
        }

        private BuildingSO currentBuilding;
        public BuildingSO CurrentBuilding {
            get { return currentBuilding; }
            set {
                currentBuilding = value;
                OnCurrentBuildingChanged?.Invoke(
                    this,
                    new OnCurrentBuildingChangedEventArgs {
                        CurrentBuilding = currentBuilding
                    }
                );
            }
        }

        private Transform hqReference;
        public Transform HQReference {
            get {
                if(hqReference == null) {
                    var hqs = GameObject.FindGameObjectsWithTag(GameObjectsTags.HQ);
                    if(hqs.Length == 0) return null;

                    hqReference = GameObject.FindGameObjectsWithTag(GameObjectsTags.HQ)[0].transform;
                }

                return hqReference;
            }
        }

        private BuildingFactorySO buildingFactory;

        void Awake() {
            Instance = this;

            buildingFactory = Resources.Load<BuildingFactorySO>("building_factory_lvl_0");
        }

        private void Start() {
            CreateHQReference();
        }

        private void CreateHQReference() {
            var hqs = GameObject.FindGameObjectsWithTag(GameObjectsTags.HQ);
            if(hqs.Length == 0) return;

            hqReference = hqs[0].transform;
            hqReference.GetComponent<Building>().HealthSystem.OnDied += (sender, args) => {
                SoundManager.Instance.PlaySound(Sound.GameOver);
                GameOverUI.Instance.Show();
            };
        }

        void Update() {
            if(Input.GetMouseButtonDown(0)) {
                var buildingPosition = CameraUtils.GetMousePosition();
                var canBuildResponse = new CanBuildRules(CurrentBuilding).CanBuild(buildingPosition);
                if(canBuildResponse.Result) {
                    ResourceManager.Instance.SpendResources(CurrentBuilding.resourceCost);
                    SoundManager.Instance.PlaySound(Sound.BuildingPlaced);
                    BuildingConstructor.Create(CurrentBuilding, buildingPosition);
                } else {
                    canBuildResponse.Reason.IfSome(
                        (reason) => TooltipUI.Instance.Show(
                            reason, new TooltipUI.TooltipTimer() { Timer = 2f }
                        )
                    );
                }
            }

            // TODO: esses ifs tem que virar uma classe de mapping de shortcuts, 
            // bem no estilo de CommandPattern
            if(Input.GetKeyDown(KeyCode.W)) {
                CurrentBuilding = buildingFactory.GetByShortcut(KeyCode.W.ToString());
            }

            if(Input.GetKeyDown(KeyCode.G)) {
                CurrentBuilding = buildingFactory.GetByShortcut(KeyCode.G.ToString());
            }

            if(Input.GetKeyDown(KeyCode.S)) {
                CurrentBuilding = buildingFactory.GetByShortcut(KeyCode.S.ToString());
            }
        }
    }
}