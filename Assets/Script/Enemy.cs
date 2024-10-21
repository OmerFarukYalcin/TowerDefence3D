using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Initial speed of the enemy, used when the enemy spawns
    [SerializeField] float startSpeed = 50f;
    private float speed;

    // Initial health of the enemy, used when the enemy spawns
    [SerializeField] float startHealth = 100;
    private float health;

    // Effect to instantiate when the enemy dies
    [SerializeField] GameObject death;

    // Reward for killing the enemy, added to player's money
    [SerializeField] int price = 50;

    // Bool to track if the enemy is dead or not
    [SerializeField] bool isDead = false;

    // Health bar UI element to display the current health
    [SerializeField] Image healthImage;

    // Initialization method called when the enemy spawns
    private void Start()
    {
        healthImage = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        // Set the speed and health to their initial values
        speed = startSpeed;
        health = startHealth;
    }

    // Method to slow down the enemy by a given percentage
    public void Slow(float _amount)
    {
        speed = startSpeed * (1f - _amount); // Reduce the speed based on the slow amount
    }

    // Method to deal damage to the enemy
    public void TakeDamage(float _amount)
    {
        health -= _amount; // Decrease the enemy's health by the given amount
        healthImage.fillAmount = health / startHealth; // Update the health bar UI

        // If health drops to zero or below, and the enemy is not already dead, call the Die method
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    // Returns the current speed of the enemy
    public float GetSpeed()
    {
        return speed;
    }

    // Returns the starting speed of the enemy
    public float GetStartSpeed()
    {
        return startSpeed;
    }

    // Sets the enemy's speed to the given value
    public void SetSpeed(float _speed)
    {
        this.speed = _speed;
    }

    // Method to handle the enemy's death
    void Die()
    {
        isDead = true; // Mark the enemy as dead
        PlayerStats.Money += price; // Add the reward to the player's money

        // Instantiate the death effect and destroy it after 3 seconds
        GameObject effect = Instantiate(death, transform.position, Quaternion.identity);
        Destroy(effect, 3f);

        // Decrease the count of alive enemies
        WaveSpawner.EnemiesAlive--;

        // Destroy the enemy object
        Destroy(gameObject);
    }
}
