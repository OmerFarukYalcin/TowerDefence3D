using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class House : MonoBehaviour
{
    public static House instance;
    [SerializeField] private float currentHealt;
    [SerializeField] Slider healtSlider;

    private void Awake()
    {
        currentHealt = healtSlider.value;
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void TakeDamage(int _damage)
    {
        currentHealt -= _damage;
        healtSlider.value = currentHealt;
        if (currentHealt <= 0)
        {
            Die();
        }
    }

    public float GetCurretHealt()
    {
        return currentHealt;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
