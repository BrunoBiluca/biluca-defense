using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour {

    private BuildingSO building;
    private float timer;
    private float timerMaxInSeconds;

    void Awake() {
        building = GetComponent<BuildingTypeHolder>().BuildingType;

        var oneSecond = 1f;
        timerMaxInSeconds = oneSecond / building.resourceGeneratorConfig.ResourcesPerSecond;
    }

    private void Start() {
        var colliders = Physics2D.OverlapCircleAll(transform.position, 5f);

        var resourceNodeCount = 0;
        foreach(var collider in colliders) {
            var resourceNode = collider.GetComponent<ResourceNode>();
            if(resourceNode != null) {
                resourceNodeCount++;
            }
        }
        Debug.Log($"Número de recursos próximos: {resourceNodeCount}");
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
