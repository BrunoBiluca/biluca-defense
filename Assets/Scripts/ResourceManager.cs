using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {
    public static ResourceManager Instance { get; set; }

    private Dictionary<string, int> resources;

    public event EventHandler OnResourceAmountChanged;

    public void Awake() {
        Instance = this;
        var resourcesList = Resources.Load<ResourceListSO>("available_resources_lvl_0");

        resources = new Dictionary<string, int>();
        resourcesList.Resources.ForEach((r) => {
            resources.Add(r.ResourceName, 0);
        });        
    }

    public int GetResourceAmount(ResourceTypeSO resource) {
        return resources[resource.ResourceName];
    }

    public void AddResource(ResourceTypeSO resource) {
        resources[resource.ResourceName] += resource.Amount;

        OnResourceAmountChanged(this, EventArgs.Empty);

        //DebugResources();
    }

    private void DebugResources() {
        foreach(var r in resources) {
            Debug.Log($"{r.Key}: {r.Value}");
        }
    }
}
