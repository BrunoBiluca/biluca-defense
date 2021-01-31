using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceGeneratorConfig {

    [SerializeField]
    private float resourcesPerSecond;
    public float ResourcesPerSecond {
        get { return resourcesPerSecond; }
        set { resourcesPerSecond = value; }
    }

    [SerializeField]
    public int maxResourceNodes;

    [SerializeField]
    public float detectionRadius;

    [SerializeField]
    private ResourceTypeSO resourceType;
    public ResourceTypeSO ResourceType {
        get { return resourceType; }
        set { resourceType = value; }
    }

}
