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
        var buildingCollider = buildingType.Prefab.GetComponent<BoxCollider2D>();
        var buildingPoint = transform.position + (Vector3)buildingCollider.offset;

        DebugDraw.DrawRectangle(buildingPoint, buildingCollider.size, Color.red);
    }
}
