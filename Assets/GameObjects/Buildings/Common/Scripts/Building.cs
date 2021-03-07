using Assets.UnityFoundation.HealthSystem;
using LanguageExt;
using UnityEngine;

namespace Assets.GameObjects.Buildings {
    public class Building : MonoBehaviour {

        public HealthSystem HealthSystem { get; private set; }

        private Transform demolishbuilding;

        void Awake() {
            var baseHealth = GetComponent<BuildingTypeHolder>().BuildingType.baseHealth;

            HealthSystem = GetComponent<HealthSystem>();
            HealthSystem.Setup(baseHealth);

            demolishbuilding = transform.Find("demolishButton");
            HideDemolishButton();
        }

        private void OnMouseEnter() {
            ShowDemolishButton();
        }

        private void OnMouseExit() {
            HideDemolishButton();
        }

        private void ShowDemolishButton() {
            if(demolishbuilding == null) return;
            demolishbuilding.gameObject.SetActive(true);
        }
        private void HideDemolishButton() {
            if(demolishbuilding == null) return;
            demolishbuilding.gameObject.SetActive(false);
        }

    }
}