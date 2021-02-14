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

    void Update() {
        if(Input.GetKeyDown(KeyCode.T)) {
            currentHealth -= 10f;
            healthBar.SetSize(currentHealth);
        }
        if(currentHealth <= 0) Destroy(gameObject);
    }
}
