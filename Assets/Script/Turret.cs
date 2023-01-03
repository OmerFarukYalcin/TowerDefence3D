using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;
    private int firePointIndex = 0;
    [Header("Use bullets (default)")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float fireRate = 2f;
    [SerializeField] float fireCountDown = 0f;
    [Header("General")]
    [SerializeField] float range = 20f;
    [Header("Use Laser")]
    [SerializeField] bool UseLaser = false;
    [SerializeField] int damageOverLifeTime = 30;
    [SerializeField] float slowAmount = 0.5f;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] ParticleSystem impactEffect;
    [SerializeField] Light ImpactLight;

    [Header("Unity Setup Fields")]
    [SerializeField] Transform[] firePoint;
    [SerializeField] Transform rotationPart;
    private string enemyTag = "Enemy";
    [SerializeField] float turnSpeed = 10f;
    void Start()
    {
        InvokeRepeating("updateTarget", 0f, 0.5f);
    }

    void Update()
    {
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

        TargetLockOn();

        if (UseLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountDown <= 0)
            {
                Shoot();
                fireCountDown = 1f / fireRate;
            }
            fireCountDown -= Time.deltaTime;
        }
    }

    void TargetLockOn()
    {
        rotationPart.rotation = Quaternion.Euler(0f, RotationTurret(target, transform).y, 0f);
    }

    void Laser()
    {
        targetEnemy.TakeDamage(damageOverLifeTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            ImpactLight.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint[0].position);
        lineRenderer.SetPosition(1, target.position);
        Vector3 dir = firePoint[0].position - target.position;
        impactEffect.transform.position = target.position + dir.normalized;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    void Shoot()
    {
        if (firePointIndex > 1)
        {
            firePointIndex = 0;
        }
        GameObject bulletObj = Instantiate(bulletPrefab, firePoint[firePointIndex].position, firePoint[firePointIndex].rotation);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (firePoint.Length > 1)
            firePointIndex++;
        if (bullet != null)
            bullet.Chase(target);
    }

    Vector3 RotationTurret(Transform _target, Transform _turret)
    {
        Vector3 dir = _target.position - _turret.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        return Quaternion.Lerp(rotationPart.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
    }

    void updateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;

        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
            target = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
