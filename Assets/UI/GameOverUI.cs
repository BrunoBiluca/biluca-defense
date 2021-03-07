using Assets.GameManagers;
using Assets.GameObjects.Enemies;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI {
    public class GameOverUI : MonoBehaviour {

        public static GameOverUI Instance { get; private set; }

        void Awake() {
            Instance = this;
            transform
                .Find("gameOverScreen")
                .Find("retryButton").GetComponent<Button>().onClick.AddListener(() => {
                GameManager.Instance.LoadGame();
            });
            transform
                .Find("gameOverScreen")
                .Find("mainMenuButton").GetComponent<Button>().onClick.AddListener(() => {
            });
        }

        public void Show() {
            transform
                .Find("gameOverScreen").gameObject.SetActive(true);
            transform
                .Find("gameOverScreen")
                .Find("waveResultText")
                .GetComponent<TMP_Text>()
                .text = $"Você sobreviveu a {EnemyWaveSpawner.Instance.WaveCounter} rodadas";
        }
    }
}