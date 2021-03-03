using Assets.UnityFoundation.HealthSystem;
using Assets.UnityFoundation.TransformUtils;
using Assets.GameObjects.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameObjects.Arrow {
    public class Arrow : MonoBehaviour {
        private const float speed = 20f;
        private const float timeOfLive = 3f;

        private float timeToDie = 0f;

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

            transform.position += movDirection * speed * Time.deltaTime;
            transform.eulerAngles = RotationUtils.GetZRotation(movDirection);

            if(target == null) {
                timeToDie += Time.deltaTime;
                if(timeToDie >= timeOfLive) Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            var enemy = collision.gameObject.GetComponent<Enemy>();
            if(enemy == null) return;

            const float damageAmount = 10f;
            enemy.HealthSystem.Damage(damageAmount);
            Destroy(gameObject);
        }
    }
}