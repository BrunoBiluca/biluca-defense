using UnityEngine;

public class ResourceGenerator : MonoBehaviour {

    private BuildingSO building;
    private ResourceGeneratorConfig resourceGeneratorConfig;

    private float timer;
    private float timerMaxInSeconds;

    void Awake() {
        building = GetComponent<BuildingTypeHolder>().BuildingType;
        resourceGeneratorConfig = building.resourceGeneratorConfig;
    }

    private void Start() {
        var colliders = Physics2D.OverlapCircleAll(
            transform.position, resourceGeneratorConfig.detectionRadius);

        var resourceNodeCount = 0;
        foreach(var collider in colliders) {
            var resourceNode = collider.GetComponent<ResourceNode>();
            if(resourceNode == null) continue;

            if(resourceNode.resourceType == resourceGeneratorConfig.ResourceType
                && resourceNodeCount < resourceGeneratorConfig.maxResourceNodes) {
                resourceNodeCount++;
            }
        }

        CalculateResourceTimer(resourceNodeCount);
    }

    private void CalculateResourceTimer(int resourceNodeCount) {
        var oneSecond = 1f;
        timerMaxInSeconds = oneSecond
            / (building.resourceGeneratorConfig.ResourcesPerSecond * resourceNodeCount);

        Debug.Log(
            $"Número de recursos próximos: {resourceNodeCount}"
            + $" Timer: {timerMaxInSeconds}"
        );
    }

    void Update() {
        timer += Time.deltaTime;

        if (timer > timerMaxInSeconds) {
            timer = 0f;
            ResourceManager
                .Instance
                .AddResource(building.resourceGeneratorConfig.ResourceType);
        }
    }
}
