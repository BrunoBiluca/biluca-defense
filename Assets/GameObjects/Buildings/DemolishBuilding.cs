using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GameObjects.Buildings {
    public class DemolishBuilding : MonoBehaviour {

        void Awake() {
            transform.Find("button").GetComponent<Button>().onClick.AddListener(() => {
                var buildingType = transform
                    .parent
                    .GetComponent<BuildingTypeHolder>()
                    .BuildingType;

                foreach(var resource in buildingType.resourceCost) {
                    ResourceManager.Instance.AddResource(
                        resource.resource, Mathf.FloorToInt(resource.amount * .6f)
                    );
                }

                Destroy(transform.parent.gameObject);
            });
        }

    }
}