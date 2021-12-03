using Assets.UnityFoundation.CameraScripts;
using UnityEngine;

namespace Assets.GameObjects.Enemies {
    public class EnemyMousePositionSpawner : MonoBehaviour {

        [SerializeField]
        private GameObject enemyPrefab;

        void Update() {
            if(Input.GetMouseButtonDown(0)) {
                SpawEnemy();
            }
        }

        private void SpawEnemy() {
            Instantiate(
                enemyPrefab,
                CameraUtils.GetMousePosition2D(),
                Quaternion.identity
            );
        }
    }
}