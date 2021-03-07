using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameManagers {
    public class ResourceManager : MonoBehaviour {
        public static ResourceManager Instance { get; set; }

        private Dictionary<ResourceTypeSO, int> resources;

        [SerializeField]
        private List<ResourceAmount> startResources;

        public event EventHandler OnResourceAmountChanged;

        public void Awake() {
            Instance = this;
            var resourcesList = Resources.Load<ResourceListSO>("available_resources_lvl_0");

            resources = new Dictionary<ResourceTypeSO, int>();
            resourcesList.Resources.ForEach((r) => {
                resources.Add(r, 0);
            });

            startResources.ForEach(amount => {
                resources[amount.resource] = amount.amount;
            });
        }

        public int GetResourceAmount(ResourceTypeSO resource) {
            return resources[resource];
        }

        public void AddResource(ResourceTypeSO resource) {
            resources[resource] += resource.Amount;

            OnResourceAmountChanged(this, EventArgs.Empty);
        }

        public void AddResource(ResourceTypeSO resource, int amount) {
            resources[resource] += amount;

            OnResourceAmountChanged(this, EventArgs.Empty);
        }

        internal void SpendResources(List<ResourceAmount> resourceCost) {
            foreach(var resourceAmount in resourceCost) {
                resources[resourceAmount.resource] -= resourceAmount.amount;
            }
            OnResourceAmountChanged(this, EventArgs.Empty);
        }
    }
}