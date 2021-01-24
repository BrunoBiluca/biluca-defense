using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour {

    public static BuildingManager Instance { get; private set; }

    private Camera mainCamera;

    private BuildingFactorySO buildingFactory;

    public BuildingSO CurrentBuilding { get; set; }

    void Awake() {
        Instance = this;
        mainCamera = Camera.main;

        buildingFactory = Resources.Load<BuildingFactorySO>("building_factory_lvl_0");
        CurrentBuilding = buildingFactory.GetByIndex(0);
    }

    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            if(CurrentBuilding != null
                && !EventSystem.current.IsPointerOverGameObject()) {

                Instantiate(CurrentBuilding.Prefab, GetWorldPosition(), Quaternion.identity);
            }
        }

        // TODO: esses ifs tem que virar uma classe de mapping de shortcuts, 
        // bem no estilo de CommandPattern
        if(Input.GetKeyDown(KeyCode.W)) {
            CurrentBuilding = buildingFactory.GetByShortcut(KeyCode.W.ToString());
        }

        if(Input.GetKeyDown(KeyCode.G)) {
            CurrentBuilding = buildingFactory.GetByShortcut(KeyCode.G.ToString());
        }

        if(Input.GetKeyDown(KeyCode.S)) {
            CurrentBuilding = buildingFactory.GetByShortcut(KeyCode.S.ToString());
        }
    }

    private Vector3 GetWorldPosition() {
        var worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0f;
        return worldPosition;
    }
}
