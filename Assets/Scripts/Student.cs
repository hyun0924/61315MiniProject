using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : MonoBehaviour
{
    SpriteRenderer sr;
    public static float atk;
    public static int studentNum;
    public static int gap;
    
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (transform.position.x > 0) sr.flipX = true;

        // StartCoroutine("attack", 5);
        studentNum++;
        if (studentNum > 10)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gap = 1;
        AtkChange();
    }

    public static void AtkChange()
    {
        atk = PlayerStat.atk / 2f;
    }

    // Animation Event
    public void Attack()
    {
        School.getInstance().GetAttackByStudent(atk);
    }
}
