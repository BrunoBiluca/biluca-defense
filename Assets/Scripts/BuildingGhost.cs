using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour {

    public GameObject GhostSprite { get; set; }

    void Awake() {
        GhostSprite = transform.Find("sprite").gameObject;
    }

    void Start() {
        BuildingManager.Instance.OnCurrentBuildingChanged += BuildingManager_OnCurrentBuildingChanged;
        Toggle(BuildingManager.Instance.CurrentBuilding);
    }

    private void BuildingManager_OnCurrentBuildingChanged(
        object sender, BuildingManager.OnCurrentBuildingChangedEventsArgs e
    ) {
        Toggle(e.CurrentBuilding);
    }

    private void Toggle(BuildingSO currentBuilding) {
        if(currentBuilding == null) {
            Hide();
        }
        else {
            Show(currentBuilding.Sprite);
        }
    }

    private void Hide() {
        GhostSprite.SetActive(false);
    }

    private void Show(Sprite sprite) {
        GhostSprite.SetActive(true);
        GhostSprite.GetComponent<SpriteRenderer>().sprite = sprite;
    }


    void Update() {
        transform.position = MouseUtils.GetWorldPosition();
    }
}
