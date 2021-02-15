using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour {

    private Rigidbody2D body;

    private Transform targetTransform;



    void Start() {
        body = GetComponent<Rigidbody2D>();

        targetTransform = BuildingManager.Instance.HQReference;
    }

    void Update() {
        MovimentHandler();
    }

    private void MovimentHandler() {
        if(targetTransform != null) {
            var movDirection = (targetTransform.position - transform.position).normalized;

            const float speed = 6f;
            body.velocity = movDirection * speed;
        }
        else {
            body.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        var buildingHealth = collision.gameObject.GetComponent<BuildingHealthHandler>();

        if(buildingHealth != null) {
            buildingHealth.Damage(10f);
            Destroy(gameObject);
        }
    }
}
