using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ResourceType")]
public class ResourceTypeSO : ScriptableObject {

    [SerializeField]
    private string resourceName;
    public string ResourceName {
        get { return resourceName; }
        set { resourceName = value; }
    }

    [SerializeField]
    private int amount;
    public int Amount {
        get { return amount; }
        set { amount = value; }
    }

    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite {
        get { return sprite; }
        set { sprite = value; }
    }

    public string shortName;

    public Color color;
}