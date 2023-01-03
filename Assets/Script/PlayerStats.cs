using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    [SerializeField] int startMoney = 400;
    [SerializeField] TextMeshProUGUI _moneyText;
    public static int Rounds;

    private void Start()
    {
        Money = startMoney;
        Rounds = 0;
    }

    private void Update()
    {
        _moneyText.text = "$" + Money;
    }
}
