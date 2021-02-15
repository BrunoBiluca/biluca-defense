
using UnityEngine;

public class ResourceNode : MonoBehaviour{
    // TODO: utilizar maxResource para determinar quando 
    // o ResourceNode deve ser� destru�do

    [SerializeField]
    private int maxResource;

    public int MaxResource {
        get { return maxResource; }
        set { maxResource = value; }
    }

    public ResourceTypeSO resourceType;

}
