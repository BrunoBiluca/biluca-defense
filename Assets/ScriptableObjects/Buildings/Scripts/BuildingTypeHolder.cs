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
}
