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
    [SerializeField] private GameObject BtnImage;

    public static AddStudentBtn Instance => instance;
    private static AddStudentBtn instance;

    Button button;
    public GameObject studentPrefab;
    public int studentNum;
    private int price;
    private Vector3[] stdpos = {
    new Vector3(-1,0), new Vector3(-1.8f,-0.75f), new Vector3(-0.41f,-1.1f),
    new Vector3(1.5f,-0.4f), new Vector3(0.4f,-0.27f), new Vector3(0.85f,-1.4f),
    new Vector3(-1.44f,-1.72f), new Vector3(2.17f,0.28f), new Vector3(-2.3f,0.12f),
    new Vector3(2.15f,-1.44f)
    };
    private AudioSource audioSource;
    private bool isReady;
    private bool isMax;
    private float resolution = 1;

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
        isReady = false;
        isMax = false;

        Instantiate(studentPrefab, stdpos[studentNum], Quaternion.identity, FriendContainer.transform);
        studentNum++;

        // Resolution
        if (Screen.width < Screen.height)
        {
            resolution = (float)Screen.width / Screen.height / (1080 / 1920f);
        }
    }

    private void Update()
    {
        if (!isReady && !isMax && Money.GetMoney() >= price)
        {
            StartCoroutine(FullFilled());
        }
    }

    private IEnumerator FullFilled()
    {
        GameObject ButtonReady = Instantiate(BtnImage, transform);
        Image image = ButtonReady.GetComponent<Image>();
        isReady = true;

        while (!isMax && Money.GetMoney() >= price)
        {
            float fadeTime = .75f;
            while (fadeTime > 0f && !isMax && Money.GetMoney() >= price)
            {
                fadeTime -= Time.deltaTime;
                image.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, fadeTime / 1f));
                float f = Mathf.Lerp(1.3f, 1, fadeTime / 0.5f);
                ButtonReady.transform.localScale = new Vector3(f, f);
                yield return null;
            }
        }

        Destroy(ButtonReady);
        isReady = false;
    }

    public void AddStudent()
    {
        if (!isMax)
        {
            if (Money.GetMoney() >= price)
            {
                Money.DecreaseMoney(price);
                price += increaseAmount;
                Instantiate(studentPrefab, stdpos[studentNum] * resolution, Quaternion.identity, FriendContainer.transform);
                studentNum++;
                audioSource.Play();
            }

            if (studentNum == stdpos.Length)
            {
                PriceText.text = "Max";
                isMax = true;
            }
            else PriceText.text = price.ToString("#,##0");
        }
    }

    public void Reset()
    {
        price = initialPrice;
        PriceText.text = price.ToString("#,##0");
        isReady = false;
        isMax = false;

        // Destroy Friends
        for (int i = FriendContainer.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(FriendContainer.transform.GetChild(i).gameObject);
        }

        studentNum = 0;
        Instantiate(studentPrefab, stdpos[studentNum] * resolution, Quaternion.identity, FriendContainer.transform);
        studentNum++;
    }
}
