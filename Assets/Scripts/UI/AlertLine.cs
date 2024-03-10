using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertLine : MonoBehaviour
{

    [SerializeField] private float delay;
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        StopCoroutine(Blink());
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        int count = 3;
        while (count > 0)
        {
            count--;
            image.color = new Color(255, 255, 255, 255);
            yield return new WaitForSeconds(delay);
            image.color = new Color(0, 0, 0, 0);
            yield return new WaitForSeconds(delay);
        }
        School.getInstance().gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
