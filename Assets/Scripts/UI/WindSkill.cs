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
        if (skillCount >= maxSkillCount)
        {
            School.getInstance().GetAttack(PlayerStat.atk * 5);
            skillCount = 0;
            windSkillGauge.fillAmount = 0;
            // Instantiate(windPrefab);
        }
        //ġ��Ÿ �߰�
    }

    public void IncreaseSkillCount()
    {
        if (skillCount >= maxSkillCount) return;

        skillCount++;
        windSkillGauge.fillAmount = (float)skillCount / maxSkillCount;

        if (skillCount >= maxSkillCount)
        {
            StartCoroutine(FullFilled());
        }
    }

    private IEnumerator FullFilled()
    {
        // windSkillFull.transform.localScale = new Vector3(1.05f, 1.05f);
        // yield return new WaitForSeconds(0.1f);
        // windSkillFull.transform.localScale = Vector3.one;
        GameObject windSkillReady = Instantiate(windSkillFull, transform);
        float fadeTime = 0.5f;
        Image image = windSkillReady.GetComponent<Image>();

        while (fadeTime > 0f)
        {
            fadeTime -= Time.deltaTime;
            image.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, fadeTime/1f));
            float f = Mathf.Lerp(1.5f, 1, fadeTime/0.5f);
            windSkillReady.transform.localScale = new Vector3(f, f);
            yield return null;
        }
    }
}
