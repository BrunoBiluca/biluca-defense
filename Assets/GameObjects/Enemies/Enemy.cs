using Assets.GameManagers;
using Assets.GameObjects.Buildings;
using Assets.UnityFoundation.HealthSystem;
using Assets.UnityFoundation.TransformUtils;
using Assets.UnityFoundation.UI.Indicators;
using UnityEngine;

namespace Assets.GameObjects.Enemies {
    public class Enemy : MonoBehaviour, IIndicable {
        public HealthSystem HealthSystem { get; private set; }

        private Rigidbody2D body;
        private TransformCircleFinder targetFinder;

        void Start() {
            body = GetComponent<Rigidbody2D>();
            targetFinder = GetComponent<TransformCircleFinder>();
            targetFinder.Setup(typeof(BuildingTypeHolder), defaultTarget: BuildingManager.Instance.HQReference);

            var baseHealth = GetComponent<EnemyTypeHolder>().Enemy.baseHealth;
            HealthSystem = GetComponent<HealthSystem>();
            HealthSystem.Setup(baseHealth);
        }

        void Update() {
            MovimentHandler();
        }

        private void MovimentHandler() {
            var movDirection = targetFinder.Target
                .Some(target => (target.position - transform.position).normalized)
                .None(Vector2.zero);

            const float speed = 6f;
            body.velocity = movDirection * speed;
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            var building = collision.gameObject.GetComponent<Building>();
            if(building == null) return;

            SoundManager.Instance.PlaySound(Sound.BuildingDamage);
            building.HealthSystem.Damage(10f);

            SoundManager.Instance.PlaySound(Sound.EnemyDie);
            Destroy(gameObject);
        }
    }
}