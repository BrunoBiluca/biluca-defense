using System;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour {

    public BuildingSO Building { get; private set; }
    private ResourceGeneratorConfig resourceGeneratorConfig;

    public float timer;
    public float timerMaxInSeconds;

    public EventHandler OnResourcesPerSecondChange;
    private float resourcesPerSecond;
    public float ResourcesPerSecond { 
        get {
            return resourcesPerSecond;
        } 
        private set {
            resourcesPerSecond = value;
            OnResourcesPerSecondChange?.Invoke(this, EventArgs.Empty);
        } 
    }

    void Awake() {
        Building = GetComponent<BuildingTypeHolder>().BuildingType;
        resourceGeneratorConfig = Building.resourceGeneratorConfig;
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
        ResourcesPerSecond = Building.resourceGeneratorConfig.ResourcesPerSecond * resourceNodeCount;
        timerMaxInSeconds = oneSecond / ResourcesPerSecond;
    }

    void Update() {
        timer += Time.deltaTime;

        if(timer > timerMaxInSeconds) {
            timer = 0f;
            ResourceManager
                .Instance
                .AddResource(Building.resourceGeneratorConfig.ResourceType);
        }
    }
}
