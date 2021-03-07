using Assets.GameManagers;
using Assets.UnityFoundation.HealthSystem;
using Assets.UnityFoundation.TimeUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.GameObjects.Buildings {

    public class BuildingRepairButton : MonoBehaviour {

        private HealthSystem healthSystem;

        [SerializeField]
        private ResourceTypeSO goldResourceType;

        void Start() {
            healthSystem = transform.parent.GetComponent<Building>().HealthSystem;

            transform.Find("button").GetComponent<Button>()
                .onClick
                .AddListener(delegate { RepairButton(); });

            healthSystem.OnFullyHeal += delegate { Hide(); };
            healthSystem.OnTakeDamage += delegate { Show(); };
        }

        private void RepairButton() {
            var repairResources = new ResourceAmount() {
                resource = goldResourceType,
                amount = 10
            };

            if(ResourceManager.Instance.CanAfford(repairResources)) {
                ResourceManager.Instance.SpendResource(repairResources);
                healthSystem.HealFull();
            } else {
                TooltipUI
                    .Instance
                    .Show(
                        "Você não tem recursos para fazer reparo nessa edificação",
                        new TooltipUI.TooltipTimer() { Timer = 2f }
                    );
            }
        }

        public void Show() {
            transform.Find("button").gameObject.SetActive(true);
        }

        public void Hide() {
            transform.Find("button").gameObject.SetActive(false);
        }
    }
}