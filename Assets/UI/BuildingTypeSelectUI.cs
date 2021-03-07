using Assets.GameManagers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour {

    [SerializeField]
    private Sprite arrowButtonSprite;
    private Transform arrowButton;

    private Dictionary<BuildingSO, Transform> buttonOptions;

    private BuildingFactorySO buildingList;
    private Transform buildingOptionTemplate;

    void Awake() {
        buttonOptions = new Dictionary<BuildingSO, Transform>();

        buildingList = Resources.Load<BuildingFactorySO>("building_factory_lvl_0");

        buildingOptionTemplate = transform.Find("buildingOptionTemplate");
        buildingOptionTemplate.gameObject.SetActive(false);

        CreateBuildingSelectUI();
    }

    void Start() {
        BuildingManager.Instance.OnCurrentBuildingChanged += BuildingManager_OnCurrentBuildingChanged;
    }

    private void BuildingManager_OnCurrentBuildingChanged(
        object sender, BuildingManager.OnCurrentBuildingChangedEventArgs e
    ) {
        UpdateBuildingSelectUI();
    }

    private void CreateBuildingSelectUI() {
        var buttonOffset = 100f;
        var index = 0;

        index = CreateSelectorButton(buttonOffset, index);

        CreateBuildingSelectorButtons(buttonOffset, index);
    }

    private int CreateSelectorButton(float buttonOffset, int index) {
        arrowButton = Instantiate(buildingOptionTemplate, transform);
        arrowButton.gameObject.SetActive(true);
        arrowButton.GetComponent<RectTransform>()
            .anchoredPosition = new Vector2(buttonOffset * index++, 0);
        arrowButton.Find("image")
            .GetComponent<Image>().sprite = arrowButtonSprite;
        arrowButton.GetComponent<Button>().onClick.AddListener(() => {
            BuildingManager.Instance.CurrentBuilding = null;
        });
        arrowButton.GetComponent<MouseEnterExitComponent>().OnMouseEnter += (object sender, EventArgs e) => {
            TooltipUI.Instance.Show("Selector");
        };
        arrowButton.GetComponent<MouseEnterExitComponent>().OnMouseExit += (object sender, EventArgs e) => {
            TooltipUI.Instance.Hide();
        };
        return index;
    }

    private int CreateBuildingSelectorButtons(
        float buttonOffset,
        int index
    ) {
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
            buildingOption.GetComponent<MouseEnterExitComponent>().OnMouseEnter += (object sender, EventArgs e) => {
                var costs = string.Join(
                    " ",
                    building.resourceCost.Select(
                        rc =>
                            $"<color=#{ColorUtility.ToHtmlStringRGB(rc.resource.color)}>" +
                            $"{rc.resource.shortName} {rc.amount}" +
                            $"</color>"
                    )
                );
                TooltipUI.Instance.Show($"{building.BuildingName}\n{costs}");
            };
            buildingOption.GetComponent<MouseEnterExitComponent>().OnMouseExit += (object sender, EventArgs e) => {
                TooltipUI.Instance.Hide();
            };

            buttonOptions.Add(building, buildingOption);
        }

        return index;
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
