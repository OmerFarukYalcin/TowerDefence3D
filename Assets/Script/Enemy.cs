using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 50f;

    private Transform target;

    private int wavePointIndex = 0;

    [SerializeField] private int damage = 5;

    private void Start()
    {
        target = wayPoints.points[0];
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;

        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNexWayPoint();
        }
    }

    void GetNexWayPoint()
    {
        if (wavePointIndex >= wayPoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        wavePointIndex++;
        target = wayPoints.points[wavePointIndex];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("house"))
        {
            print("fffff");
            other.gameObject.GetComponent<House>().TakeDamage(damage);
        }
    }

}