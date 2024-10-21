using UnityEngine;
using UnityEngine.UI;

public class House : MonoBehaviour
{
    // Singleton instance of the House class
    public static House instance;

    // Current health of the house
    [SerializeField] private float currentHealth;

    // Reference to the health bar UI (Slider)
    [SerializeField] Slider healthSlider;

    // Called when the script instance is being loaded
    private void Awake()
    {
        // Set the current health to match the slider's initial value
        currentHealth = healthSlider.value;

        // Implement Singleton pattern to ensure only one instance of the House exists
        if (instance != null)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        else
        {
            instance = this; // Assign this instance to the static variable
        }
    }

    // Method to reduce health when the house takes damage
    public void TakeDamage(int _damage)
    {
        // Reduce the house's health by the damage amount
        currentHealth -= _damage;

        // Update the health slider to reflect the new health value
        healthSlider.value = currentHealth;

        // If health reaches zero or below, call the Die method
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Getter method to retrieve the current health of the house
    public float GetCurretHealt()
    {
        return currentHealth;
    }

    // Method to handle what happens when the house is destroyed
    void Die()
    {
        // Destroy the house object
        Destroy(gameObject);
    }
}
