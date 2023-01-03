using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavePointIndex = 0;
    [SerializeField] private int damage = 5;
    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = wayPoints.points[0];
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;

        transform.Translate(dir.normalized * enemy.GetSpeed() * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNexWayPoint();
        }
        enemy.SetSpeed(enemy.GetStartSpeed());
    }

    void GetNexWayPoint()
    {
        if (wavePointIndex >= wayPoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        wavePointIndex++;
        target = wayPoints.points[wavePointIndex];
    }

    void EndPath()
    {
        if (House.instance.GetCurretHealt() > 0)
            House.instance.TakeDamage(damage);
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }


}
