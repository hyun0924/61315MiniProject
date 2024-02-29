using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class WindSkill : MonoBehaviour
{
    [SerializeField] private GameObject windSkillFull;
    [SerializeField] private int maxSkillCount;

    private Image windSkillGauge;
    private int skillCount;
    public GameObject windPrefab;

    private void Awake()
    {
        windSkillGauge = windSkillFull.GetComponent<Image>();
        windSkillGauge.fillAmount = 0;
    }

    public void OnMouseDown()
    {
        if (skillCount >= maxSkillCount && School.getInstance().gameObject.activeSelf)
        {
            skillCount = 0;
            windSkillGauge.fillAmount = 0;
            Instantiate(windPrefab, new Vector3(0, -6), Quaternion.identity);
        }
    }

    public void IncreaseSkillCount()
    {
        if (skillCount >= maxSkillCount) return;

        skillCount++;
        windSkillGauge.fillAmount = (float)skillCount / maxSkillCount;

        if (skillCount >= maxSkillCount)
        {
            StopCoroutine(FullFilled());
            StartCoroutine(FullFilled());
        }
    }

    private IEnumerator FullFilled()
    {
        GameObject windSkillReady = Instantiate(windSkillFull, transform);
        Image image = windSkillReady.GetComponent<Image>();

        while(skillCount >= maxSkillCount)
        {
            float fadeTime = 1f;
            while (fadeTime > 0f)
            {
                fadeTime -= Time.deltaTime;
                image.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, fadeTime / 1f));
                float f = Mathf.Lerp(1.5f, 1, fadeTime / 0.5f);
                windSkillReady.transform.localScale = new Vector3(f, f);
                yield return null;
            }
        }
    }
}
