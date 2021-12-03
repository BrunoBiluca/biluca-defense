using Assets.GameObjects.Enemies;
using System;
using TMPro;
using UnityEngine;

namespace Assets.UI {
    public class EnemyWaveUI : MonoBehaviour {

        private TextMeshProUGUI waveText;
        private TextMeshProUGUI waveTimerText;

        private void Awake() {
            waveText = transform.Find("waveText").GetComponent<TextMeshProUGUI>();
            waveTimerText = transform.Find("waveTimerText").GetComponent<TextMeshProUGUI>();
        }

        void Start() {
            UpdateWaveText();
            EnemyWaveSpawner.Instance.OnWaveChanged += OnWaveChanged;
        }

        private void OnWaveChanged(object sender, EventArgs args) {
            UpdateWaveText();
        }

        private void UpdateWaveText() {
            waveText.text = $"Wave {EnemyWaveSpawner.Instance.WaveCounter}";
        }

        private void Update() {
            var timerValue = EnemyWaveSpawner.Instance
                .EnemiesSpawnerTimer.RemainingTime.ToString("0.0");
            waveTimerText.SetText($"Próxima wave: {timerValue}s");
        }
    }
}