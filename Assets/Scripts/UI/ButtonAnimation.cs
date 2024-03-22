using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    [SerializeField] private float pauseTime;
    RectTransform rectTransform;
    Button button;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.RemoveAllListeners();
        StartCoroutine(ScaleUp());
    }
    

    private IEnumerator ScaleUp()
    {
        yield return new WaitForSecondsRealtime(pauseTime);
        button.onClick.AddListener(GameManager.Instance.Resume);

        float scale = 0;
        float currentTime = 0;
        while (scale < 1)
        {
            currentTime += Time.unscaledDeltaTime;
            scale = Mathf.Lerp(0f, 1f, currentTime/pauseTime);
            rectTransform.localScale = new Vector3(scale, scale);
            yield return null;
        }
    }

    private IEnumerator ChangeScale()
    {
        float currentTime = 0;
        float trigger = 1;

        while (true)
        {
            currentTime += Time.unscaledDeltaTime * trigger;
            float scale = Mathf.Lerp(1f, 1.05f, currentTime/0.3f);
            rectTransform.localScale = new Vector3(scale, scale);

            if (currentTime >= 0.5f || currentTime <= 0f) trigger *= -1;

            yield return null;
        }
    }
}
