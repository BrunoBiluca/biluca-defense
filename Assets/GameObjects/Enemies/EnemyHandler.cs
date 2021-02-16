using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour {

    private Rigidbody2D body;
    private AreaTargetFinder targetFinder;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        targetFinder = GetComponent<AreaTargetFinder>();
        targetFinder.Setup(BuildingManager.Instance.HQReference, typeof(BuildingTypeHolder));
    }

    void Update() {
        MovimentHandler();
    }

    private void MovimentHandler() {
        targetFinder.Target
            .Some(target => {
                if(target == null) return;

                var movDirection = (target.position - transform.position).normalized;

                const float speed = 6f;
                body.velocity = movDirection * speed;
            })
            .None(() => body.velocity = Vector2.zero);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        var buildingHealth = collision.gameObject.GetComponent<BuildingHealthHandler>();

        if(buildingHealth != null) {
            buildingHealth.Damage(10f);
            Destroy(gameObject);
        }
    }
}
