using Assets.Foundation;
using Assets.Foundation.CameraUtils;
using Assets.Foundation.TransformUtils;
using System;
using UnityEngine;

namespace Assets.GameObjects.Enemies {
    public class EnemyWaveSpawner : MonoBehaviour {

        public static EnemyWaveSpawner Instance { get; private set; }

        [SerializeField]
        private GameObject enemyPrefab;

        [SerializeField]
        private GameObject emenySpawnIndicatorPrefab;
        public GameObject SpawnIndicator { get; private set; }

        private Vector3 HQPosition;

        public EventHandler OnWaveChanged;
        public int WaveCounter { get; private set; } = 0;
        private readonly int enemiesPerWave = 5;
        private readonly int enemiesIncreasePerWave = 3;

        public Timer EnemiesSpawnerTimer { get; private set; }

        private void Awake() {
            Instance = this;
        }

        void Start() {
            HQPosition = BuildingManager.Instance.HQReference.position;

            EnemiesSpawnerTimer = new Timer("Enemies Spawner Timer", 10f, SpawnEnemies);
            OnWaveChanged?.Invoke(this, EventArgs.Empty);
        }

        private void SpawnEnemies() {
            WaveCounter++;

            var spawnPosition = PositionUtils.GetRandomSemiCirclePosition(HQPosition, 30f, 60f);

            Destroy(SpawnIndicator);
            SpawnIndicator = Instantiate(
                emenySpawnIndicatorPrefab,
                spawnPosition,
                Quaternion.identity
            );
            OnWaveChanged?.Invoke(this, EventArgs.Empty);


            var enemiesThisWave = enemiesPerWave + enemiesIncreasePerWave * WaveCounter;
            for(int i = 0; i < enemiesThisWave; i++) {
                Instantiate(
                    enemyPrefab,
                    PositionUtils.GetRandomCirclePosition(spawnPosition, 1f),
                    Quaternion.identity
                );
            }
        }
    }
}