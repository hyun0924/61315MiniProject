using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    private static int money;
    private void Start()
    {
            money = 0;
    }
    public static int GetMoney()
    {
        return money;
    }
    public static void IncreaseMoney(int inc)
    {
        money += inc;
    }
}
