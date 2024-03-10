using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class AddStudentBtn : MonoBehaviour
{
    [SerializeField] private int initialPrice;
    [SerializeField] private int increaseAmount;
    [SerializeField] private TextMeshProUGUI PriceText;
    [SerializeField] private GameObject FriendContainer;

    public static AddStudentBtn Instance => instance;
    private static AddStudentBtn instance;

    Button button;
    public GameObject studentPrefab;
    public int studentNum;
    private int price;
    private Vector3[] stdpos = {
    new Vector3(-1,0), new Vector3(-1.8f,-0.75f), new Vector3(-0.41f,-1.1f),
    new Vector3(1.5f,-0.4f), new Vector3(0.4f,-0.27f), new Vector3(0.85f,-1.4f),
    new Vector3(-1.44f,-1.72f), new Vector3(2.17f,0.08f), new Vector3(-2.3f,0.12f),
    new Vector3(2.15f,-1.44f)
    };
    private AudioSource audioSource;

    public AddStudentBtn()
    {
        instance = this;
    }

    private void Awake()
    {
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();
        price = initialPrice;
        PriceText.text = price.ToString("#,##0");
        button.onClick.AddListener(AddStudent);

        Instantiate(studentPrefab, stdpos[studentNum], Quaternion.identity);
        studentNum++;
    }

    public void AddStudent()
    {
        if (studentNum < stdpos.Length)
        {
            if (Money.GetMoney() >= price)
            {
                Money.DecreaseMoney(price);
                price += increaseAmount;
                Instantiate(studentPrefab, stdpos[studentNum], Quaternion.identity, FriendContainer.transform);
                studentNum++;
                audioSource.Play();
            }

            if (studentNum == stdpos.Length) PriceText.text = "Max";
            else PriceText.text = price.ToString("#,##0");
        }
    }

    public void Reset()
    {
        price = initialPrice;
        PriceText.text = price.ToString("#,##0");

        // Destroy Friends
        for (int i = FriendContainer.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(FriendContainer.transform.GetChild(i).gameObject);
        }
    }
}
