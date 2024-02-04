using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTouch : MonoBehaviour
{
    [SerializeField] private WindSkill windSkill;
    public void OnMouseDown()
    {
        Debug.Log("touched");
        windSkill.IncreaseSkillCount();
        bool crit = Random.Range(0, 50) == 0;
        School.getInstance().GetAttack(crit ? PlayerStat.atk*10 : PlayerStat.atk);
        //Ä¡¸íÅ¸ Ãß°¡
    }
}
