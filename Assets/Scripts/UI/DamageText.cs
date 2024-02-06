using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private float fadeTime;
    TextMeshProUGUI textMesh;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeOut());
        textMesh.text = PlayerStat.atk.ToString("#,##0");
    }

    private IEnumerator FadeOut()
    {
        Color color = textMesh.color;
        float time = fadeTime;
        while (time > 0f)
        {
            time -= Time.deltaTime;
            transform.position += new Vector3(0, 30, 0) * Time.deltaTime;
            textMesh.color = new Color(color.r, color.g, color.b, Mathf.Lerp(0, color.a, time / fadeTime));

            yield return null;
        }

        Destroy(gameObject);
    }
}
