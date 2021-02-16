using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    [SerializeField]
    private float arrowsPerSecond;

    [SerializeField]
    private GameObject arrowPrefab;

    private AreaTransformFinder targetFinder;

    private float arrowSpawTimerMax;
    private float arrowSpawTimer;
    private Transform arrowSpawner;

    void Start() {
        arrowSpawner = transform.Find("arrowSpawner");

        arrowSpawTimerMax = 1f / arrowsPerSecond;

        targetFinder = GetComponent<AreaTransformFinder>();
        targetFinder.Setup(null, typeof(Enemy), 20f);
    }

    void Update() {
        arrowSpawTimer += Time.deltaTime;
        if(arrowSpawTimer > arrowSpawTimerMax) {
            arrowSpawTimer = 0f;

            targetFinder.Target
                .IfSome(target => {
                    Instantiate(arrowPrefab, arrowSpawner.position, Quaternion.identity)
                    .GetComponent<Arrow>()
                    .Setup(target.gameObject.GetComponent<Enemy>());
                });
        }

    }
}
