using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class WindSkill : MonoBehaviour
{
    [SerializeField] private Image windSkillGauge;
    [SerializeField] private int maxSkillCount;
    private int skillCount;
    public GameObject windPrefab;
    private void Awake()
    {
        windSkillGauge.fillAmount = 0;
    }

    public void OnMouseDown()
    {
        if (skillCount >= maxSkillCount)
        {
            School.getInstance().GetAttack(PlayerStat.atk * 5);
            skillCount = 0;
            windSkillGauge.fillAmount = 0;
            Instantiate(windPrefab);
        }
        //치명타 추가
    }

    public void IncreaseSkillCount()
    {
        skillCount = math.min(maxSkillCount, skillCount + 1);
        windSkillGauge.fillAmount = (float)skillCount / maxSkillCount;
    }
}
