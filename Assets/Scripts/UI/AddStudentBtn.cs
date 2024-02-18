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
    new Vector3(-1,0,0), new Vector3(0, 1, 0), new Vector3(1, 2, 0),
    new Vector3(-1,3,0),new Vector3(0,4,0),new Vector3(1,5,0),
    new Vector3(-1,6,0),new Vector3(0,7,0),new Vector3(1,8,0),
    new Vector3(-1,9,0)
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
        if (studentNum <= 9)
        {
            if (Money.GetMoney() >= price)
            {
                Money.DecreaseMoney(price);
                price += 500;
                PriceText.text = price.ToString("#,##0");
                Instantiate(studentPrefab, stdpos[studentNum], Quaternion.identity);
                studentNum++;
            }
        }
    }
}
