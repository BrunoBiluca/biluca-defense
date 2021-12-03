using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.GameManagers {
    public class GameManager : MonoBehaviour {

        public static GameManager instance;
        public static GameManager Instance {
            get { 
                if(instance == null) {
                    instance = FindObjectOfType<GameManager>();
                    if(instance == null) {
                        var gameManager = new GameObject("GameManager");
                        instance = gameManager.AddComponent<GameManager>();
                    }
                }
                return instance;
            } 
        }

        [SerializeField]
        private bool debugMode;
        public bool DebugMode {
            get { return debugMode; }
            set { debugMode = value; }
        }

        internal void LoadGame() {
            SceneManager.LoadScene("GameScene");
        }

        internal void LoadMainMenu() {
            SceneManager.LoadScene("MainMenu");
        }
    }
}