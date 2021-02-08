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

    private const float constructionRadius = 18f;
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
            var buildingPosition = MouseUtils.GetWorldPosition();
            if(CanBuild(buildingPosition)) {
                Instantiate(CurrentBuilding.Prefab, buildingPosition, Quaternion.identity);                
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

    private bool CanBuild(Vector3 position) {
        if(CurrentBuilding == null) return false;
        if(EventSystem.current.IsPointerOverGameObject()) return false;
        if(HasBuildingBeneth(position)) return false;
        if(HasSameBuildingTypeClose(position)) return false;
        if(!HasBuildingOnRangeForConstruction(position)) return false;

        return true;
    }

    private bool HasBuildingBeneth(Vector3 position) {
        var currentBuildingCollider = CurrentBuilding.Prefab.GetComponent<BoxCollider2D>();
        var collidersBeneathBuilding = Physics2D.OverlapBoxAll(
            position + (Vector3)currentBuildingCollider.offset,
            currentBuildingCollider.size,
            0f
        );
        foreach(var building in collidersBeneathBuilding) {
            var buildingTypeHolder = building.GetComponent<BuildingTypeHolder>();
            if(buildingTypeHolder != null) {
                return true;
            }
        }

        return false;
    }

    private bool HasSameBuildingTypeClose(Vector3 position) {
        var collidersSameBuilding = Physics2D.OverlapCircleAll(
            position,
            currentBuilding.resourceGeneratorConfig.detectionRadius
        );
        foreach(var building in collidersSameBuilding) {
            var buildingTypeHolder = building.GetComponent<BuildingTypeHolder>();
            if(buildingTypeHolder == null) continue;

            if(buildingTypeHolder.BuildingType == currentBuilding) {
                return true;
            }
        }

        return false;
    }

    private bool HasBuildingOnRangeForConstruction(Vector3 position) {
        var collidersSameBuilding = Physics2D.OverlapCircleAll(
            position,
            constructionRadius
        );
        foreach(var building in collidersSameBuilding) {
            var buildingTypeHolder = building.GetComponent<BuildingTypeHolder>();
            if(buildingTypeHolder != null) return true;
        }

        return false;        
    }

}
