using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : MonoBehaviour
{

    public static int atk;

    public static int gap;
    private void Start()
    {
        gap = 1;
        AtkChange();
    }

    public static void AtkChange()
    {
        //��ġ ����
        atk = PlayerStat.atk-gap;
    }
}
