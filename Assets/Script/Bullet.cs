using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Target to which the bullet is heading
    private Transform target;

    // Effect to be instantiated upon impact
    [SerializeField] GameObject impactEffect;

    // Damage dealt to the target
    [SerializeField] int _damage = 50;

    // Speed at which the bullet travels
    [SerializeField] float speed = 70f;

    // Radius for explosion (for AoE damage), if set to a value greater than 0
    [SerializeField] float explosionRadius = 0f;

    // Method to set the target the bullet should chase
    public void Chase(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        // If there is no target, destroy the bullet
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Calculate the direction to the target
        Vector3 dir = target.position - transform.position;

        // Calculate how far the bullet travels this frame
        float distanceThisFrame = speed * Time.deltaTime;

        // If the bullet reaches or surpasses the target, hit the target
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Move the bullet towards the target
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        // Rotate the bullet to face the target
        transform.LookAt(target);
    }

    // Method called when the bullet hits the target
    void HitTarget()
    {
        // Instantiate the impact effect at the bullet's position
        GameObject hitEffect = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(hitEffect, 2f); // Destroy the effect after 2 seconds

        // Check if the bullet has an explosion radius for AoE damage
        if (explosionRadius > 0)
        {
            Explode(); // Apply AoE damage to nearby enemies
        }
        else
        {
            Damage(target); // Apply direct damage to the targeted enemy
        }

        // Destroy the bullet after hitting the target
        Destroy(gameObject);
    }

    // Method to apply explosion damage in a radius
    void Explode()
    {
        // Find all colliders in the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        // Loop through each collider and apply damage to enemies
        foreach (var collider in colliders)
        {
            if (collider.tag.Equals("Enemy"))
            {
                Damage(collider.transform);
            }
        }
    }

    // Method to apply damage to a specific enemy
    void Damage(Transform enemy)
    {
        // Get the Enemy component and apply damage if it exists
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(_damage);
        }
    }

    // Method to visually represent the explosion radius in the Unity Editor (only visible when selected)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
