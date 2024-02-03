using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATKUPBtn : MonoBehaviour
{
    public int price;
    private void Awake()
    {
        price = 5;
    }
    private void OnMouseDown()
    {
        if (Money.GetMoney() >= price)
        {
            Money.DecreaseMoney(price);
            price += 5;
            PlayerStat.atk += 5;//일단 임시로 5로했음
        }

    }
}
