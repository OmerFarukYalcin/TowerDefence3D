using UnityEngine;

public class Turret : MonoBehaviour
{
    // Variables to track the target and the enemy associated with the target
    private Transform target;
    private Enemy targetEnemy;

    // Index to alternate between fire points when shooting
    private int firePointIndex = 0;

    [Header("Use bullets (default)")]
    // Prefab for the bullets the turret shoots
    [SerializeField] GameObject bulletPrefab;

    // Fire rate of the turret (bullets per second)
    [SerializeField] float fireRate = 2f;

    // Countdown timer to control firing
    [SerializeField] float fireCountDown = 0f;

    [Header("General")]
    // Range of the turret in which it can detect and target enemies
    [SerializeField] float range = 20f;

    [Header("Use Laser")]
    // Boolean to determine if the turret uses a laser instead of bullets
    [SerializeField] bool UseLaser = false;

    // Damage dealt per second when using a laser
    [SerializeField] int damageOverLifeTime = 30;

    // Amount to slow the enemy when hit by the laser
    [SerializeField] float slowAmount = 0.5f;

    // Components for the laser effect
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] ParticleSystem impactEffect;
    [SerializeField] Light ImpactLight;

    [Header("Unity Setup Fields")]
    // Array of points from where the turret fires bullets or lasers
    [SerializeField] Transform[] firePoint;

    // The part of the turret that rotates to follow the target
    [SerializeField] Transform rotationPart;

    // Tag to identify enemies
    private string enemyTag = "Enemy";

    // Speed at which the turret rotates to follow the target
    [SerializeField] float turnSpeed = 10f;

    // Called when the turret is initialized
    void Start()
    {
        // Continuously look for targets, updating every 0.5 seconds
        InvokeRepeating("updateTarget", 0f, 0.5f);
    }

    // Called once per frame to handle firing and target locking
    void Update()
    {
        // If there is no target, turn off the laser effects
        if (target == null)
        {
            if (UseLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    ImpactLight.enabled = false;
                }
            }
            return;
        }

        // Lock on to the target
        TargetLockOn();

        // If the turret uses a laser, damage the enemy over time
        if (UseLaser)
        {
            Laser();
        }
        else
        {
            // Otherwise, shoot bullets if the countdown has reached 0
            if (fireCountDown <= 0)
            {
                Shoot();
                fireCountDown = 1f / fireRate; // Reset the countdown based on fire rate
            }

            // Decrease the fire countdown
            fireCountDown -= Time.deltaTime;
        }
    }

    // Method to rotate the turret to face the target
    void TargetLockOn()
    {
        // Rotate the turret towards the target using Lerp for smooth movement
        rotationPart.rotation = Quaternion.Euler(0f, RotationTurret(target, transform).y, 0f);
    }

    // Method to handle laser attacks
    void Laser()
    {
        // Apply damage to the enemy over time
        targetEnemy.TakeDamage(damageOverLifeTime * Time.deltaTime);

        // Slow the enemy
        targetEnemy.Slow(slowAmount);

        // Activate laser effects if they are not already active
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            ImpactLight.enabled = true;
        }

        // Set the laser's start and end positions
        lineRenderer.SetPosition(0, firePoint[0].position);
        lineRenderer.SetPosition(1, target.position);

        // Calculate the direction for the impact effect
        Vector3 dir = firePoint[0].position - target.position;
        impactEffect.transform.position = target.position + dir.normalized;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    // Method to shoot bullets from the turret
    void Shoot()
    {
        // Alternate between fire points if there are multiple fire points
        if (firePointIndex > 1)
        {
            firePointIndex = 0;
        }

        // Instantiate the bullet at the current fire point
        GameObject bulletObj = Instantiate(bulletPrefab, firePoint[firePointIndex].position, firePoint[firePointIndex].rotation);
        Bullet bullet = bulletObj.GetComponent<Bullet>();

        // Move to the next fire point if there are multiple
        if (firePoint.Length > 1)
            firePointIndex++;

        // If the bullet exists, set it to chase the target
        if (bullet != null)
            bullet.Chase(target);
    }

    // Method to calculate the rotation for the turret to face the target
    Vector3 RotationTurret(Transform _target, Transform _turret)
    {
        Vector3 dir = _target.position - _turret.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        return Quaternion.Lerp(rotationPart.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
    }

    // Method to update the target based on range and distance to enemies
    void updateTarget()
    {
        // Find all enemies in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // Loop through all enemies and find the closest one
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        // If a nearby enemy is found within range, set it as the target
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null; // Reset target if no enemy is within range
        }
    }

    // Method to visualize the turret's range in the Unity editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
