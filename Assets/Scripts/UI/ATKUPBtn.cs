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
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        PriceText.text = initialPrice.ToString("#,##0");
        button.onClick.AddListener(OnMouseDown);
    }
    private void OnMouseDown()
    {
        if (Money.GetMoney() >= initialPrice)
        {
            Money.DecreaseMoney(initialPrice);
            initialPrice += increaseAmount;
            PriceText.text = initialPrice.ToString("#,##0");
            PlayerStat.atk *= 1.3f;
            Student.AtkChange();
        }
    }
}
