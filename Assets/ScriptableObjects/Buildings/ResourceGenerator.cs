using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour {

    private BuildingSO building;
    private float timer;
    private float timerMaxInSeconds;

    void Start() {
        building = GetComponent<BuildingTypeHolder>().BuildingType;

        var oneSecond = 1f;
        timerMaxInSeconds = oneSecond / building.resourceGeneratorConfig.ResourcesPerSecond;
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
