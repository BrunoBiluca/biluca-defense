using UnityEngine;

public class BuildingHealthHandler : MonoBehaviour {

    private HealthBar healthBar;

    private float currentHealth;

    void Start() {
        var baseHealth = GetComponent<BuildingTypeHolder>().BuildingType.baseHealth;

        healthBar = transform.Find("healthBar").GetComponent<HealthBar>();
        healthBar.Setup(baseHealth);

        currentHealth = baseHealth;
    }

    public void Damage(float amount) {
        currentHealth -= amount;
        healthBar.SetSize(currentHealth);

        if(currentHealth <= 0) Destroy(gameObject);
    }
}
