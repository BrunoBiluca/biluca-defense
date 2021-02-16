using UnityEngine;

public class Enemy : MonoBehaviour {
    public HealthSystem HealthSystem { get; private set; }

    private Rigidbody2D body;
    private AreaTransformFinder targetFinder;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        targetFinder = GetComponent<AreaTransformFinder>();
        targetFinder.Setup(BuildingManager.Instance.HQReference, typeof(BuildingTypeHolder));

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

        building.HealthSystem.Damage(10f);
        Destroy(gameObject);
    }
}
