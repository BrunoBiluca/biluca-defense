using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ResourceList")]
public class ResourceListSO : ScriptableObject {

    [SerializeField]
    private List<ResourceTypeSO> resources;

    public List<ResourceTypeSO> Resources {
        get { return resources; }
    }

}
