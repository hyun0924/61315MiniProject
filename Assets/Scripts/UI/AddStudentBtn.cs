using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStudentBtn : MonoBehaviour
{
    public int price;
    public GameObject studentPrefab;
    public int studentNum;
    private Vector3[] stdpos = {
    new Vector3(0,0,0), new Vector3(0, 0, 0), new Vector3(0, 0, 0),
    new Vector3(0,0,0),new Vector3(0,0,0),new Vector3(0,0,0),
    new Vector3(0,0,0),new Vector3(0,0,0),new Vector3(0,0,0),
    new Vector3(0,0,0)
    };//여기다 배치
    private void Awake()
    {
        price = 100;
        studentNum = 0;
    }
    private void OnMouseDown()
    {
        if (studentNum <= 9)
        {
        if (Money.GetMoney() >= price)
            {
                Money.DecreaseMoney(price);
                price += 100;
                Instantiate(studentPrefab, stdpos[studentNum], Quaternion.identity);
                studentNum++;
            }
        }
    }
}
