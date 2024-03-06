using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ATKUPBtn : MonoBehaviour
{
    [SerializeField] private int initialPrice;
    [SerializeField] private int increaseAmount;
    [SerializeField] private TextMeshProUGUI PriceText;

    public static ATKUPBtn Instance => instance;
    private static ATKUPBtn instance;

    public ATKUPBtn()
    {
        instance = this;
    }

    Button button;
    private int price;

    private void Awake()
    {
        button = GetComponent<Button>();
        price = initialPrice;
        PriceText.text = price.ToString("#,##0");
        button.onClick.AddListener(OnMouseDown);
    }
    private void OnMouseDown()
    {
        if (Money.GetMoney() >= price)
        {
            Money.DecreaseMoney(price);
            price += increaseAmount;
            PriceText.text = price.ToString("#,##0");
            PlayerStat.Instance.IncreaseAtk();
            Student.AtkChange();
        }
    }

    public void Reset()
    {
        price = initialPrice;
        PriceText.text = price.ToString("#,##0");
        PlayerStat.Instance.Reset();
    }
}
