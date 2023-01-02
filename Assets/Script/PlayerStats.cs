using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 400;
    public TextMeshProUGUI _moneyText;

    private void Start()
    {
        Money = startMoney;
    }

    private void Update()
    {
        _moneyText.text = "$" + Money;
    }
}
