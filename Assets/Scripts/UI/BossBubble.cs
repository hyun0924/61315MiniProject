using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossBubble : MonoBehaviour
{
    [SerializeField] private float stayTime;
    [SerializeField] private float fadeTime;
    Image image;
    TextMeshProUGUI textMesh;
    RectTransform rectTransform;

    public float bubbleTime => stayTime + fadeTime;

    private void Awake()
    {
        image = GetComponent<Image>();
        textMesh = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(Animation());

        rectTransform = GetComponent<RectTransform>();
        rectTransform.position = new Vector3(Random.Range(-120f, 120f), Random.Range(-450f, 500f));
    }

    public void SetText(string script)
    {
        textMesh.text = script;
    }

    private IEnumerator Animation()
    {
        yield return new WaitForSeconds(stayTime);
        yield return StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        Color color = image.color;
        float time = fadeTime;
        while (time > 0f)
        {
            time -= Time.deltaTime;
            image.color = new Color(color.r, color.g, color.b, Mathf.Lerp(0, color.a, time / fadeTime));
            textMesh.color = new Color(0, 0, 0, Mathf.Lerp(0, color.a, time / fadeTime));

            yield return null;
        }

        Destroy(gameObject);
    }
}
