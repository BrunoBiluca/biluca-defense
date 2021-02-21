using Assets.Foundation.HealthSystem;
using UnityEngine;

public class Building : MonoBehaviour {

    public HealthSystem HealthSystem { get; private set; }

    void Start() {
        var baseHealth = GetComponent<BuildingTypeHolder>().BuildingType.baseHealth;

        HealthSystem = GetComponent<HealthSystem>();
        HealthSystem.Setup(baseHealth);
    }
}
