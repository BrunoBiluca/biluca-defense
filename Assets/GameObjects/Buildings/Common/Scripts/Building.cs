using Assets.GameManagers;
using Assets.UnityFoundation.PostProcessing;
using Assets.UnityFoundation.Systems.HealthSystem;
using UnityEngine;

namespace Assets.GameObjects.Buildings {
    public class Building : MonoBehaviour {

        public HealthSystem HealthSystem { get; private set; }

        private Transform demolishbuilding;

        void Awake() {
            var baseHealth = GetComponent<BuildingTypeHolder>().BuildingType.baseHealth;

            HealthSystem = GetComponent<HealthSystem>();
            HealthSystem.Setup(baseHealth);

            HealthSystem.OnTakeDamage += (sender, args) => {
                CinemachineCameraShake.Instance.Shake();
                ChromaticAberrationHandler.Instance.FullWeight();
            };
            HealthSystem.OnDied += (sender, args) => {
                Instantiate(
                    GameAssets.Instance.buildingDestroyedParticles, 
                    transform.position, 
                    Quaternion.identity
                );
                SoundManager.Instance.PlaySound(Sound.BuildingDestroyed);
            };

            demolishbuilding = transform.Find("demolishButton");
            HideDemolishButton();

            transform
                .Find("buildingRepairButton")
                .GetComponent<BuildingRepairButton>()
                .Hide();
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