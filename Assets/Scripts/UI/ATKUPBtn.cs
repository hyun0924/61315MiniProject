using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ATKUPBtn : MonoBehaviour
{
    public int price;
    [SerializeField] private TextMeshProUGUI PriceText;
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        PriceText.text = price.ToString("#,##0");
        button.onClick.AddListener(OnMouseDown);
    }
    private void OnMouseDown()
    {
        if (Money.GetMoney() >= price)
        {
            Money.DecreaseMoney(price);
            price += 200;
            PriceText.text = price.ToString("#,##0");
            PlayerStat.atk *= 1.3f;
            Student.AtkChange();
        }
    }
}
