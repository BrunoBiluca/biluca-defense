using System;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    public static BuildingManager Instance { get; private set; }

    public event EventHandler<OnCurrentBuildingChangedEventsArgs> OnCurrentBuildingChanged;
    public class OnCurrentBuildingChangedEventsArgs : EventArgs {
        public BuildingSO CurrentBuilding { get; set; }
    }

    private BuildingSO currentBuilding;
    public BuildingSO CurrentBuilding {
        get { return currentBuilding; }
        set {
            currentBuilding = value;
            OnCurrentBuildingChanged?.Invoke(
                this,
                new OnCurrentBuildingChangedEventsArgs {
                    CurrentBuilding = currentBuilding
                }
            ); ;
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

    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            var buildingPosition = WorldPositionUtils.GetMousePosition();
            var canBuildResponse = new CanBuildRules(CurrentBuilding).CanBuild(buildingPosition);
            if(canBuildResponse.Result) {
                ResourceManager.Instance.SpendResources(CurrentBuilding.resourceCost);
                Instantiate(CurrentBuilding.Prefab, buildingPosition, Quaternion.identity);         
            }
            else {
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
