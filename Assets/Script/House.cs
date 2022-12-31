using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class House : MonoBehaviour
{
    [SerializeField] private float currentHealt;
    [SerializeField] Slider healtSlider;

    private void Awake()
    {
        currentHealt = healtSlider.value;
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

    void Die()
    {
        Destroy(gameObject);
    }
}
