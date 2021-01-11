using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    [SerializeField]
    private Transform pfWoodHarvester;

    private Camera mainCamera;

    void Start() {
        mainCamera = Camera.main;
    }

    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            Instantiate(pfWoodHarvester, GetWorldPosition(), Quaternion.identity);
        }
    }

    private Vector3 GetWorldPosition() {
        var worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0f;
        return worldPosition;
    }
}
