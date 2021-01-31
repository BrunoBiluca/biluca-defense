
using UnityEngine;

public class ResourceNode : MonoBehaviour{
    // TODO: utilizar maxResource para determinar quando 
    // o ResourceNode deve será destruído

    [SerializeField]
    private int maxResource;

    public int MaxResource {
        get { return maxResource; }
        set { maxResource = value; }
    }

    public ResourceTypeSO resourceType;

}
