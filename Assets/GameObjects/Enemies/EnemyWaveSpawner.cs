using Assets.UnityFoundation.CameraUtils;
using Assets.UnityFoundation.TransformUtils;
using Assets.UnityFoundation.TimeUtils;
using System;
using UnityEngine;
using Assets.GameManagers;

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
        private readonly int enemiesIncreasePerWave = 2;

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
                WaittingCoroutine.RealSeconds(.3f);
                Instantiate(
                    enemyPrefab,
                    PositionUtils.GetRandomCirclePosition(spawnPosition, 1f),
                    Quaternion.identity
                );
            }
        }
    }
}