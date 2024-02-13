using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : MonoBehaviour
{

    public static int atk;
    public static int studentNum;
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
    private void Awake()
    {
        StartCoroutine("attack", 5);
        studentNum++;
        if (studentNum > 10)
        {
            Destroy(gameObject);
        }


    }
    IEnumerator attack(float delay)
    {
        
        // School.getInstance().GetAttackByStudent(atk);
        yield return new WaitForSeconds(delay);
        StartCoroutine("attack", 5);
    }
}
