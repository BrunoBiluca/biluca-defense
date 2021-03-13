using Assets.UnityFoundation;
using Assets.UnityFoundation.CameraScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameObjects.Enemies {
    public class EnemyMousePositionSpawner : MonoBehaviour {

        [SerializeField]
        private GameObject enemyPrefab;

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
}