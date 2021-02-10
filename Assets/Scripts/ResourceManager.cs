using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {
    public static ResourceManager Instance { get; set; }

    private Dictionary<ResourceTypeSO, int> resources;

    public event EventHandler OnResourceAmountChanged;

    public void Awake() {
        Instance = this;
        var resourcesList = Resources.Load<ResourceListSO>("available_resources_lvl_0");

        resources = new Dictionary<ResourceTypeSO, int>();
        resourcesList.Resources.ForEach((r) => {
            resources.Add(r, 0);
        });        
    }

    public int GetResourceAmount(ResourceTypeSO resource) {
        return resources[resource];
    }

    public void AddResource(ResourceTypeSO resource) {
        resources[resource] += resource.Amount;

        OnResourceAmountChanged(this, EventArgs.Empty);

        //DebugResources();
    }

    private void DebugResources() {
        foreach(var r in resources) {
            Debug.Log($"{r.Key}: {r.Value}");
        }
    }

    internal void SpendResources(List<ResourceAmount> resourceCost) {
        foreach(var resourceAmount in resourceCost) {
            resources[resourceAmount.resource] -= resourceAmount.amount;
        }
    }
}
