using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprint : MonoBehaviour
{
    [SerializeField] private float fadeTime;
    [SerializeField] private GameObject DamageTextObject;
    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-35f, 35f));
        StartCoroutine(FadeOut());
    }

    public void CriticalSize()
    {
        transform.localScale = Vector3.one;
    }

    public void SetDamageText(bool crit)
    {
        DamageTextObject.GetComponent<DamageText>().SetText(crit ? PlayerStat.atk * 10 : PlayerStat.atk, 0);
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
