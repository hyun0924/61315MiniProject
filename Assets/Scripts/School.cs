using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class School : MonoBehaviour
{
    private int HP;
    public int MaxHP;
    private static  School instance = new School();
    public static School getInstance()
    {
        return instance;
    }
    private void Start()
    {
        HP = MaxHP;
    }
    public void ReGen()
    {
        HP = MaxHP;
    }
    public void GetAttack(int dmg)
    {
        HP-=dmg;
        if(HP < 0 )
        {
            //대충 죽는 처리&&돈
        }
        //대충 애니메이션 
    }
}
