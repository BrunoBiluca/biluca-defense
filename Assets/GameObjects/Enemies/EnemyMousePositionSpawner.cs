using Assets.Foundation;
using Assets.Foundation.CameraUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMousePositionSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject enemyPrefab;

    void Start() {

    }

    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            SpawEnemy();
        }
    }

    private void SpawEnemy() {
        Instantiate(
            enemyPrefab,
            CameraUtils.GetMousePosition(),
            Quaternion.identity
        );
    }
}
