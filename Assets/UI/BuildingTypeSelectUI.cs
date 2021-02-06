using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour {

    [SerializeField]
    private Sprite arrowButtonSprite;
    private Transform arrowButton;

    private Dictionary<BuildingSO, Transform> buttonOptions;

    void Awake() {
        buttonOptions = new Dictionary<BuildingSO, Transform>();
        CreateBuildingSelectUI();
    }

    void Start() {
        BuildingManager.Instance.OnCurrentBuildingChanged += BuildingManager_OnCurrentBuildingChanged;
    }

    private void BuildingManager_OnCurrentBuildingChanged(
        object sender, BuildingManager.OnCurrentBuildingChangedEventsArgs e
    ) {
        UpdateBuildingSelectUI();
    }

    private void CreateBuildingSelectUI() {
        var buildingList = Resources.Load<BuildingFactorySO>("building_factory_lvl_0");

        var buildingOptionTemplate = transform.Find("buildingOptionTemplate");
        buildingOptionTemplate.gameObject.SetActive(false);

        var buttonOffset = 100f;

        var index = 0;

        arrowButton = Instantiate(buildingOptionTemplate, transform);
        arrowButton.gameObject.SetActive(true);
        arrowButton.GetComponent<RectTransform>()
            .anchoredPosition = new Vector2(buttonOffset * index++, 0);
        arrowButton.Find("image")
            .GetComponent<Image>().sprite = arrowButtonSprite;
        arrowButton.GetComponent<Button>().onClick.AddListener(() => {
            BuildingManager.Instance.CurrentBuilding = null;
        });

        foreach(var building in buildingList.possibleBuildings) {
            if(!building.isAvailableToBuild) continue;

            var buildingOption = Instantiate(buildingOptionTemplate, transform);
            buildingOption.gameObject.SetActive(true);

            buildingOption
                .GetComponent<RectTransform>()
                .anchoredPosition = new Vector2(buttonOffset * index++, 0);
            buildingOption
                .Find("image")
                .GetComponent<Image>().sprite = building.Sprite;

            buildingOption.GetComponent<Button>().onClick.AddListener(() => {
                BuildingManager.Instance.CurrentBuilding = building;
            });

            buttonOptions.Add(building, buildingOption);
        }
    }

    private void UpdateBuildingSelectUI() {
        arrowButton.Find("selected").gameObject.SetActive(false);
        foreach(var option in buttonOptions) {
            option.Value.Find("selected").gameObject.SetActive(false);
        }

        var currentBuilding = BuildingManager.Instance.CurrentBuilding;
        if(currentBuilding == null) {
            arrowButton.Find("selected").gameObject.SetActive(true);
        } else {
            buttonOptions[currentBuilding].Find("selected").gameObject.SetActive(true);
        }
    }
}
