using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprint : MonoBehaviour
{
    [SerializeField] private float fadeTime;
    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        Color color = sr.color;
        float time = fadeTime;
        while (time > 0f)
        {
            time -= Time.deltaTime;
            sr.color = new Color(color.r, color.g, color.b, Mathf.Lerp(0, color.a, time / fadeTime));

            yield return null;
        }

        Destroy(gameObject);
    }
}
