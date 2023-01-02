using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 50f;
    public float startHealt = 100;
    private float healt;
    public GameObject death;
    private Transform target;
    public int price = 50;
    private int wavePointIndex = 0;

    public Image healtImage;

    [SerializeField] private int damage = 5;

    private void Start()
    {
        target = wayPoints.points[0];
        healt = startHealt;
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
        Destroy(gameObject);
    }

    public void TakeDamage(int _amount)
    {
        healt -= _amount;
        healtImage.fillAmount = healt / startHealt;
        if (healt <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStats.Money += price;
        GameObject effect = Instantiate(death, transform.position, Quaternion.identity);
        Destroy(effect, 3f);
        Destroy(gameObject);
    }

}