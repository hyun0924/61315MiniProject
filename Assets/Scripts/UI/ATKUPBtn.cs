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
            PlayerStat.atk += 5;//�ϴ� �ӽ÷� 5������
        }

    }
}
