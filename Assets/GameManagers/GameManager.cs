using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.GameManagers {
    public class GameManager : MonoBehaviour {

        public static GameManager Instance { get; private set; }

        [SerializeField]
        private bool debugMode;
        public bool DebugMode {
            get { return debugMode; }
            set { debugMode = value; }
        }

        private void Awake() {
            Instance = this;
        }

        internal void LoadGame() {
            SceneManager.LoadScene("GameScene");
        }
    }
}