using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTouch : MonoBehaviour
{
    public void OnMouseDown()
    {
        bool crit = Random.Range(0, 50) == 0;
        School.getInstance().GetAttack(crit ? PlayerStat.atk*10 : PlayerStat.atk);
        //치명타 추가
        
    }
}
