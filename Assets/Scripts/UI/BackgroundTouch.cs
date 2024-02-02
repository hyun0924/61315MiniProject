using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTouch : MonoBehaviour
{
    private void OnMouseDown()
    {
        School.getInstance().GetAttack(PlayerStat.atk);
        //치명타 추가
    }
}
