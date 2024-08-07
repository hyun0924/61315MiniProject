using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleAnimation : MonoBehaviour
{
    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;
    [SerializeField] private float changeTime;
    [SerializeField] private float stayTime;
    [SerializeField] private bool BurningTimeObject = false;

    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        rectTransform.localScale = new Vector3(minScale, minScale);
        StartCoroutine(ChangeScale());
    }

    private IEnumerator ChangeScale()
    {
        float currentTime = 0;
        float trigger = 1;
        float entireTime = changeTime + stayTime;
        float count = 2;

        float timescale = 1;

        while (!BurningTimeObject || (BurningTimeObject && count > 0))
        {
            if (BurningTimeObject) timescale = Time.timeScale;
            currentTime = Mathf.Min(entireTime, Mathf.Max(0, currentTime + timescale * Time.unscaledDeltaTime * trigger));
            float scale = Mathf.Lerp(minScale, maxScale, currentTime / changeTime);
            rectTransform.localScale = new Vector3(scale, scale);
            
            if (currentTime >= entireTime || currentTime <= 0f)
            {
                if (BurningTimeObject) count--;
                trigger *= -1;
            }

            yield return null;
        }
    }
}
