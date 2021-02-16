using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    private const float speed = 20f;
    Enemy target;

    public void Setup(Enemy target) {
        this.target = target;
    }

    Vector3 lastDirection;

    private void Start() {
        lastDirection = Vector3.zero;
    }

    void Update() {
        var movDirection = target != null ?
            (target.transform.position - transform.position).normalized
            : lastDirection;

        lastDirection = movDirection;

        transform.position = movDirection * speed * Time.deltaTime;        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if(enemy == null) return;

        const float damageAmount = 10f;
        enemy.HealthSystem.Damage(damageAmount);
        Destroy(gameObject);
    }
}
