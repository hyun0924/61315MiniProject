using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class School : MonoBehaviour
{
    protected double HP;
    public double MaxHP;
    protected static School instance = null;
    public static bool levelUp;
    public static int stack;
    void Awake()
    {
        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;
            instance.MaxHP = 100d;
            School.levelUp = false;
            School.stack = 0;
            //씬 전환이 되더라도 파괴되지 않게 한다.
            //gameObject만으로도 이 스크립트가 컴포넌트로서 붙어있는 Hierarchy상의 게임오브젝트라는 뜻이지만, 
            //나는 헷갈림 방지를 위해 this를 붙여주기도 한다.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 GameMgr이 존재할 수도 있다.
            //그럴 경우엔 이전 씬에서 사용하던 인스턴스를 계속 사용해주는 경우가 많은 것 같다.
            //그래서 이미 전역변수인 instance에 인스턴스가 존재한다면 자신(새로운 씬의 GameMgr)을 삭제해준다.
            Destroy(this.gameObject);
        }
    }//긁어온겁니다.
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
        School.stack++;
        if (levelUp)
        {
            MaxHP *= 1.1;
            levelUp = false;
            School.stack = 0;
        }
        else if (School.stack >= 19) levelUp = true;
       
       
        //보스 체크
        
        
        HP = !levelUp?MaxHP:MaxHP*5;
        
        
    }
    public void GetAttack(int dmg)
    {
        HP-=dmg;
        Debug.Log("Attacked!" + dmg + "damge");
        if(HP < 0 )
        {
            //대충 죽는 처리
            Money.IncreaseMoney(Random.Range(2, 4));
            if (levelUp)
            {
                Money.IncreaseMoney(Random.Range(2, 4));
                Money.IncreaseMoney(Random.Range(2, 4));
                Money.IncreaseMoney(Random.Range(2, 4));
                Money.IncreaseMoney(Random.Range(2, 4));
            }
        }
        //대충 애니메이션 
    }
}
