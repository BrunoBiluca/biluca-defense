using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Building")]
public class BuildingSO : ScriptableObject {
    [SerializeField]
    private string buildingName;
    public string BuildingName {
        get { return buildingName; }
        set { buildingName = value; }
    }

    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite {
        get { return sprite; }
        set { sprite = value; }
    }

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

    public ResourceGeneratorConfig resourceGeneratorConfig;
}
