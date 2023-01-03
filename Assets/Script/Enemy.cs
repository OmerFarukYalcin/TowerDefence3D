using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float startSpeed = 50f;
    private float speed;
    [SerializeField] float startHealt = 100;
    private float healt;
    [SerializeField] GameObject death;
    [SerializeField] int price = 50;
    [SerializeField] bool isDead = false;

    [SerializeField] Image healtImage;


    private void Start()
    {
        speed = startSpeed;
        healt = startHealt;
    }

    public void Slow(float _amount)
    {
        speed = startSpeed * (1f - _amount);
    }

    public void TakeDamage(float _amount)
    {
        healt -= _amount;
        healtImage.fillAmount = healt / startHealt;
        if (healt <= 0 && !isDead)
        {
            Die();
        }
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetStartSpeed()
    {
        return startSpeed;
    }

    public void SetSpeed(float _speed)
    {
        this.speed = _speed;
    }

    void Die()
    {
        isDead = true;
        PlayerStats.Money += price;
        GameObject effect = Instantiate(death, transform.position, Quaternion.identity);
        Destroy(effect, 3f);
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }

}