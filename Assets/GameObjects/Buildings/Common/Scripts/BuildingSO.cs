using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Building")]
public class BuildingSO : ScriptableObject {

    public float baseHealth;

    [SerializeField]
    private string buildingName;
    public string BuildingName {
        get { return buildingName; }
        set { buildingName = value; }
    }

    public bool isAvailableToBuild;

    public bool itGenerateResources;

    [SerializeField]
    private Transform prefab;
    public Transform Prefab {
        get { return prefab; }
        set { prefab = value; }
    }

    [SerializeField]
    private string shortcut;
    public string Shortcut {
        get { return shortcut; }
        set { shortcut = value; }
    }


    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite {
        get { return sprite; }
        set { sprite = value; }
    }

    public ResourceGeneratorConfig resourceGeneratorConfig;

    public List<ResourceAmount> resourceCost;
}
