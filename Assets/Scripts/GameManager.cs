using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
