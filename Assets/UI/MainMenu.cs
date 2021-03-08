using Assets.GameManagers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    void Start() {
        transform.Find("playButton").GetComponent<Button>().onClick.AddListener(() => {
            GameManager.Instance.LoadGame();
        });
        transform.Find("quitButton").GetComponent<Button>().onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
