using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTypeHolder : MonoBehaviour {
    [SerializeField]
    private BuildingSO buildingType;
    public BuildingSO BuildingType {
        get { return buildingType; }
        set { buildingType = value; }
    }

    private void Update() {
        if(GameManager.Instance.DebugMode) {
            var buildingCollider = buildingType.Prefab.GetComponent<BoxCollider2D>();
            DebugDraw.DrawRectangle(
                transform.position + (Vector3)buildingCollider.offset, 
                buildingCollider.size, 
                Color.red
            );
            DebugDraw.DrawCircle(
                transform.position, 
                buildingType.resourceGeneratorConfig.detectionRadius, 
                Color.blue
            );
        }
    }
}
