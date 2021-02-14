using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour {

    public GameObject GhostSprite { get; set; }

    public BuildingTypeHolder holder;

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
        holder.BuildingType = currentBuilding;
        if(currentBuilding == null) {
            Hide();
        }
        else {
            Show(currentBuilding.Sprite);
        }
    }

    private void Hide() {
        foreach(Transform child in transform) {
            child.gameObject.SetActive(false);
        }        
    }

    private void Show(Sprite sprite) {
        foreach(Transform child in transform) {
            child.gameObject.SetActive(true);
        } 
        GhostSprite.GetComponent<SpriteRenderer>().sprite = sprite;
    }


    void Update() {
        transform.position = MouseUtils.GetWorldPosition();
    }
}