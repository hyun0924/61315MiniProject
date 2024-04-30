using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurningGauge : MonoBehaviour
{
    private static bool isBurning;
    public static bool IsBurning => isBurning;
    private float burningPower;
    [SerializeField] private float maxBurningPower;
    [SerializeField] private float burningDuration;
    [SerializeField] private Image BurningGaugeFull;
    [SerializeField] private Sprite BurningOnSprite;
    [SerializeField] private Sprite BurningOffSprite;
    [SerializeField] private GameObject BurningTimeObject;

    private static BurningGauge instance;
    public static BurningGauge Instance => instance;

    BurningGauge()
    {
        instance = this;
    }

    private void Awake()
    {
        isBurning = false;
        Reset();
    }

    public void ChargeBurningPower()
    {
        if (isBurning) return;
        burningPower++;
        BurningGaugeFull.fillAmount = burningPower / maxBurningPower;

        if (burningPower >= maxBurningPower) BurningTime();
    }

    private void BurningTime()
    {
        isBurning = true;
        BurningGaugeFull.GetComponent<Image>().sprite = BurningOnSprite;
        StartCoroutine(Burning());
    }

    private IEnumerator Burning()
    {
        float remainTime = burningDuration;
        BurningTimeObject.SetActive(true);

        while (remainTime > 0f && isBurning)
        {
            remainTime -= Time.deltaTime;
            BurningGaugeFull.fillAmount = remainTime / burningDuration;
            yield return null;
        }

        isBurning = false;
        burningPower = 0;
        BurningGaugeFull.GetComponent<Image>().sprite = BurningOffSprite;
        BurningTimeObject.SetActive(false);

    }

    public void Reset()
    {
        isBurning = false;
        burningPower = 0;
        BurningGaugeFull.fillAmount = 0f;
        BurningGaugeFull.GetComponent<Image>().sprite = BurningOffSprite;
        BurningTimeObject.SetActive(false);
    }
}
