using Assets.GameObjects.Enemies;
using Assets.UnityFoundation.Code;
using UnityEngine;

namespace Assets.UI {
    public class EnemyIndicator : MonoBehaviour {

        private RectTransform rectTransform;
        private GameObject image;

        private void Awake() {
            rectTransform = GetComponent<RectTransform>();
            image = transform.Find("image").gameObject;
        }

        void Update() {
            UpdateWaveIndicator();
        }

        private void UpdateWaveIndicator() {
            var spawnIndicator = EnemyWaveSpawner.Instance.SpawnIndicator;
            if(spawnIndicator == null || spawnIndicator.GetComponent<Renderer>().isVisible) {
                image.SetActive(false);
                return;
            } else {
                image.SetActive(true);
            }

            var spawnPosition = spawnIndicator.transform.position;
            var direction = (spawnPosition - Camera.main.transform.position).normalized;

            rectTransform.anchoredPosition = direction * 300f;
            rectTransform.eulerAngles = RotationUtils.GetZRotation(direction);
        }
    }
}