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
    [SerializeField] private float minClickTime;
    [SerializeField] private GameObject BtnImage;

    public static ATKUPBtn Instance => instance;
    private static ATKUPBtn instance;
    private AudioSource audioSource;
    private bool isClick;
    private float clickTime;

    public ATKUPBtn()
    {
        instance = this;
    }

    Button button;
    private int price;
    private bool isReady;

    private void Awake()
    {
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();
        price = initialPrice;
        PriceText.text = price.ToString("#,##0");
        isClick = false;
        clickTime = 0f;
        isReady = false;
    }

    private void Update()
    {
        if (isClick && clickTime < minClickTime)
        {
            clickTime += Time.deltaTime;
            if (clickTime >= minClickTime) StartCoroutine(RepeatUpgrade());
        }

        if (!isReady && Money.GetMoney() >= price)
        {
            StartCoroutine(FullFilled());
        }
    }

    private IEnumerator FullFilled()
    {
        GameObject ButtonReady = Instantiate(BtnImage, transform);
        Image image = ButtonReady.GetComponent<Image>();
        isReady = true;

        while (Money.GetMoney() >= price)
        {
            float fadeTime = .75f;
            while (fadeTime > 0f && Money.GetMoney() >= price)
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

    public void PointerDown()
    {
        isClick = true;
        clickTime = 0f;

        if (Money.GetMoney() >= price)
        {
            Upgrade();
        }
    }

    public void PointerUp()
    {
        isClick = false;
        clickTime = 0f;
    }

    private void Upgrade()
    {
        Money.DecreaseMoney(price);
        price += increaseAmount;
        PriceText.text = price.ToString("#,##0");
        PlayerStat.Instance.IncreaseAtk();
        Student.AtkChange();
        audioSource.Play();
    }

    private IEnumerator RepeatUpgrade()
    {
        WaitForSeconds seconds = new WaitForSeconds(0.1f);
        while (Money.GetMoney() >= price && clickTime > 0f)
        {
            Upgrade();
            yield return seconds;
        }
    }

    public void Reset()
    {
        price = initialPrice;
        PriceText.text = price.ToString("#,##0");
        PlayerStat.Instance.Reset();
    }
}
