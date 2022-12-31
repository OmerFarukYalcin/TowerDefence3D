using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform[] firePoint;
    [SerializeField] Transform rotationPart;
    private string enemyTag = "Enemy";
    [SerializeField] float range = 20f;
    [SerializeField] float turnSpeed = 10f;
    [SerializeField] float fireRate = 2f;
    [SerializeField] float fireCountDown = 0f;
    private int firePointIndex = 0;
    void Start()
    {
        InvokeRepeating("updateTarget", 0f, 0.5f);
    }

    void Update()
    {
        if (target == null)
            return;

        rotationPart.rotation = Quaternion.Euler(0f, RotationTurret(target, transform).y, 0f);

        if (fireCountDown <= 0)
        {
            Shoot();
            fireCountDown = 1f / fireRate;
        }
        fireCountDown -= Time.deltaTime;
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
