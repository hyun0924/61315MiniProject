using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class School : MonoBehaviour
{
    protected int HP;
    public int MaxHP;
    protected static  School instance = new School();
    public static School getInstance()
    {
        return instance;
    }
    protected void Start()
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
            //���� �״� ó��
            Money.IncreaseMoney(Random.Range(2, 4));
        }
        //���� �ִϸ��̼� 
    }
}
