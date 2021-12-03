using Assets.GameManagers;
using Assets.GameObjects.Buildings;
using LanguageExt;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanBuildRules {

    private BuildingSO currentBuilding;
    private const float constructionRadius = 10f;

    public CanBuildRules(BuildingSO currentBuilding) {
        this.currentBuilding = currentBuilding;
    }

    public CanBuildResponse CanBuild(Vector3 position) {
        if(currentBuilding == null) 
            return CanBuildResponse.New(false);

        if(EventSystem.current.IsPointerOverGameObject()) 
            return CanBuildResponse.New(false);

        if(HasBuildingBeneth(position)) 
            return CanBuildResponse.New(false, "Area is not clear");

        if(HasSameBuildingTypeClose(position))
            return CanBuildResponse.New(false, "Too close to another building from same type");

        if(!HasBuildingOnRangeForConstruction(position))
            return CanBuildResponse.New(false, "Too long from another building"); ;

        if(!CanAfford()) 
            return CanBuildResponse.New(false, "Not enougth resources, stranger");

        return CanBuildResponse.Ok();
    }

    private bool HasBuildingBeneth(Vector3 position) {
        var currentBuildingCollider = currentBuilding.Prefab.GetComponent<BoxCollider2D>();
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

            var buildingConstruction = building.GetComponent<BuildingConstructor>();
            if(buildingConstruction != null) {
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

    private bool CanAfford() {
        foreach(var resourceAmount in currentBuilding.resourceCost) {
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
