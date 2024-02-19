using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class AddStudentBtn : MonoBehaviour
{
    public int price;
    [SerializeField] private TextMeshProUGUI PriceText;
    Button button;
    public GameObject studentPrefab;
    public int studentNum;
    private Vector3[] stdpos = {
    new Vector3(-1,0), new Vector3(-1.8f,-0.75f), new Vector3(-0.41f,-1.1f),
    new Vector3(1.5f,-0.4f), new Vector3(0.4f,-0.27f), new Vector3(0.85f,-1.4f),
    new Vector3(-1.44f,-1.72f), new Vector3(2.17f,0.08f), new Vector3(-2.3f,0.12f),
    new Vector3(2.15f,-1.44f)
    };//¿©±â´Ù ¹èÄ¡
    private void Awake()
    {
        studentNum = 0;
        button = GetComponent<Button>();
        PriceText.text = price.ToString("#,##0");
        button.onClick.AddListener(OnMouseDown);
    }

    private void OnMouseDown()
    {
        if (studentNum < stdpos.Length)
        {
            if (Money.GetMoney() >= price)
            {
                Money.DecreaseMoney(price);
                price += 500;
                Instantiate(studentPrefab, stdpos[studentNum], Quaternion.identity);
                studentNum++;
            }

            if (studentNum == stdpos.Length) PriceText.text = "Max";
            else PriceText.text = price.ToString("#,##0");
        }
    }
}
