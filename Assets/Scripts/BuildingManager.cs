using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

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

    private BuildingFactorySO buildingFactory;

    void Awake() {
        Instance = this;

        buildingFactory = Resources.Load<BuildingFactorySO>("building_factory_lvl_0");
        CurrentBuilding = buildingFactory.GetByIndex(0);
    }

    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            if(CanBuild()) {
                Instantiate(CurrentBuilding.Prefab, MouseUtils.GetWorldPosition(), Quaternion.identity);
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

    private bool CanBuild() {
        if(CurrentBuilding == null) return false;
        if(EventSystem.current.IsPointerOverGameObject()) return false;

        var currentBuildingCollider = CurrentBuilding.Prefab.GetComponent<BoxCollider2D>();
        var buildingPoint = transform.position + (Vector3)currentBuildingCollider.offset;
        var collidersBeneathBuilding = Physics2D.OverlapBoxAll(
            buildingPoint,
            currentBuildingCollider.size,
            0f
        );

        return true;
    }
}
