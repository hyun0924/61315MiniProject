using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    private static int money;
    [SerializeField] private TextMeshProUGUI MoneyText;
    private void Start()
    {
        money = 0;
        MoneyText.text = money.ToString("#,##0");
    }
    public static int GetMoney()
    {
        return money;
    }
    public static void IncreaseMoney(int inc)
    {
        money += inc;

    }
    public static void DecreaseMoney(int dec)
    {
        money -= dec;
    }

    private void Update()
    {
        MoneyText.text = money.ToString("#,##0");
    }
}
