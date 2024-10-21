using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Reference to the current target waypoint the enemy is moving towards
    private Transform target;

    // Index of the current waypoint in the list
    private int wavePointIndex = 0;

    // Amount of damage the enemy deals when it reaches the end of its path
    [SerializeField] private int damage = 5;

    // Reference to the Enemy script for speed and other properties
    private Enemy enemy;

    void Start()
    {
        // Get the Enemy component from this GameObject
        enemy = GetComponent<Enemy>();

        // Set the first waypoint as the target
        target = wayPoints.points[0];
    }

    private void Update()
    {
        // Calculate the direction from the enemy's position to the target waypoint
        Vector3 dir = target.position - transform.position;

        // Move the enemy towards the target, adjusting its speed with Time.deltaTime for frame rate independence
        transform.Translate(dir.normalized * enemy.GetSpeed() * Time.deltaTime, Space.World);

        // If the enemy is close enough to the target waypoint, proceed to the next waypoint
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }

        // Reset the enemy's speed after each frame to its starting speed (may be useful if enemy gets slowed temporarily)
        enemy.SetSpeed(enemy.GetStartSpeed());
    }

    // Method to move to the next waypoint in the list
    void GetNextWayPoint()
    {
        // If the enemy has reached the final waypoint, call EndPath (enemy reaches the base)
        if (wavePointIndex >= wayPoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        // Otherwise, move to the next waypoint
        wavePointIndex++;
        target = wayPoints.points[wavePointIndex];
    }

    // Method called when the enemy reaches the end of the path (usually the player's base)
    void EndPath()
    {
        // If the house (player's base) has health left, apply damage
        if (House.instance.GetCurretHealt() > 0)
            House.instance.TakeDamage(damage); // House takes damage when the enemy reaches it

        // Decrease the number of alive enemies
        WaveSpawner.EnemiesAlive--;

        // Destroy the enemy GameObject
        Destroy(gameObject);
    }
}
