using Assets.UnityFoundation.Code;
using UnityEngine;

namespace Assets.GameObjects.Enemies {
    public class EnemyRandomSpawner : MonoBehaviour {

        [SerializeField]
        private GameObject enemyPrefab;

        private readonly float timerMax = .5f;
        private float timer;

        void Update() {
            timer += Time.deltaTime;

            if(timer > timerMax) {
                timer = 0;
                SpawEnemy();
            }
        }

        private void SpawEnemy() {
            Instantiate(
                enemyPrefab,
                PositionUtils.GetRandomPosition(20f),
                Quaternion.identity
            );
        }
    }
}