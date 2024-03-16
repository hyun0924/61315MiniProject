using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartBtn : MonoBehaviour
{
    RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        StartCoroutine(ChangeScale());
    }

    private IEnumerator ChangeScale()
    {
        float currentTime = 0;
        float trigger = 1;

        while (true)
        {
            currentTime += Time.deltaTime * trigger;
            float scale = Mathf.Lerp(1f, 1.05f, currentTime/0.3f);
            rectTransform.localScale = new Vector3(scale, scale);

            if (currentTime >= 0.5f || currentTime <= 0f) trigger *= -1;

            yield return null;
        }
    }
}
