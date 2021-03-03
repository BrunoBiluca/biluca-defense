using Assets.UnityFoundation.HealthSystem;
using UnityEngine;

namespace Assets.GameObjects.Buildings {
    public class Building : MonoBehaviour {

        public HealthSystem HealthSystem { get; private set; }

        void Start() {
            var baseHealth = GetComponent<BuildingTypeHolder>().BuildingType.baseHealth;

            HealthSystem = GetComponent<HealthSystem>();
            HealthSystem.Setup(baseHealth);
        }
    }
}