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
    }

    public void SetText(float dmg, float moveSpeed)
    {
        textMesh.text = dmg.ToString("#,##0");
        StartCoroutine(FadeOut(moveSpeed));
    }

    private IEnumerator FadeOut(float moveSpeed)
    {
        Color color = textMesh.color;
        float time = fadeTime;
        while (time > 0f)
        {
            time -= Time.deltaTime;
            transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime;
            textMesh.color = new Color(color.r, color.g, color.b, Mathf.Lerp(0, color.a, time / fadeTime));
            //transform.localScale = new Vector3(time, time);

            yield return null;
        }

        Destroy(gameObject);
    }
}
