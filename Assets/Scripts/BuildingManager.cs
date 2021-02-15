using LanguageExt;
using System;
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
        CurrentBuilding = buildingFactory.GetByIndex(0);
    }

    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            var buildingPosition = WorldPositionUtils.GetMousePosition();
            var canBuildResponse = CanBuild(buildingPosition);
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

    private CanBuildResponse CanBuild(Vector3 position) {
        if(CurrentBuilding == null) return CanBuildResponse.New(false);
        if(EventSystem.current.IsPointerOverGameObject()) return CanBuildResponse.New(false);
        if(HasBuildingBeneth(position)) return CanBuildResponse.New(false, "Area is not clear");
        if(HasSameBuildingTypeClose(position)) 
            return CanBuildResponse.New(false, "Too close to another building from same type");
        if(!HasBuildingOnRangeForConstruction(position)) 
            return CanBuildResponse.New(false, "Too long from another building");;
        if(!CanAfford()) return CanBuildResponse.New(false, "Not enougth resources, stranger");

        return CanBuildResponse.Ok();
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
            CurrentBuilding.resourceGeneratorConfig.detectionRadius
        );
        foreach(var building in collidersSameBuilding) {
            var buildingTypeHolder = building.GetComponent<BuildingTypeHolder>();
            if(buildingTypeHolder == null) continue;

            if(buildingTypeHolder.BuildingType == CurrentBuilding) {
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

    private bool CanAfford() {
        foreach(var resourceAmount in CurrentBuilding.resourceCost){
            var currentAmount = ResourceManager.Instance.GetResourceAmount(resourceAmount.resource);
            if(resourceAmount.amount > currentAmount) {
                return false;
            }
        }

        return true;
    }

    public class CanBuildResponse {

        public static CanBuildResponse New(bool result, string reason = null) {
            return new CanBuildResponse() { 
                Result = result, 
                Reason = reason != null ? Option<string>.Some(reason) : Option<string>.None
            };
        }

        internal static CanBuildResponse Ok() {
            return new CanBuildResponse() { Result = true };
        }

        public bool Result { get; set; }

        public Option<string> Reason { get; set; }

    }
}
