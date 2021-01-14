using UnityEngine;

public class BuildingManager : MonoBehaviour {

    private Camera mainCamera;

    private BuildingFactorySO buildingFactory;
    private BuildingSO currentBuilding;

    void Start() {
        mainCamera = Camera.main;

        buildingFactory = Resources.Load<BuildingFactorySO>("BuildingFactorySO");
        currentBuilding = buildingFactory.GetByIndex(0);
    }

    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            Instantiate(currentBuilding.Prefab, GetWorldPosition(), Quaternion.identity);
        }

        // TODO: esses ifs tem que virar uma classe de mapping de shortcuts, 
        // bem no estilo de CommandPattern
        if(Input.GetKeyDown(KeyCode.W)) {
            Debug.Log(KeyCode.W.ToString());
            currentBuilding = buildingFactory.GetByShortcut(KeyCode.W.ToString());
        }

        if(Input.GetKeyDown(KeyCode.G)) {
            currentBuilding = buildingFactory.GetByShortcut(KeyCode.G.ToString());
        }

        if(Input.GetKeyDown(KeyCode.S)) {
            currentBuilding = buildingFactory.GetByShortcut(KeyCode.S.ToString());
        }
    }

    private Vector3 GetWorldPosition() {
        var worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0f;
        return worldPosition;
    }
}
