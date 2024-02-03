using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSkill : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (BackgroundTouch.skillCount >= 50)
        {
            School.getInstance().GetAttack( PlayerStat.atk * 5);
            BackgroundTouch.skillCount = 0;
        }
        //치명타 추가
    }
}
