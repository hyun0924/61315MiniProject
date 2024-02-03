using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class School : MonoBehaviour
{
    protected float HP;
    public float SchoolHP => HP;
    public float MaxHP;
    protected static School instance = null;
    public static bool levelUp;
    public static int stack;

    [SerializeField] private GameObject SchoolHPPrefab;

    void Awake()
    {
        if (null == instance)
        {
            //�� Ŭ���� �ν��Ͻ��� ź������ �� �������� instance�� ���ӸŴ��� �ν��Ͻ��� ������� �ʴٸ�, �ڽ��� �־��ش�.
            instance = this;
            School.levelUp = false;
            School.stack = 0;
            //�� ��ȯ�� �Ǵ��� �ı����� �ʰ� �Ѵ�.
            //gameObject�����ε� �� ��ũ��Ʈ�� ������Ʈ�μ� �پ��ִ� Hierarchy���� ���ӿ�����Ʈ��� ��������, 
            //���� �򰥸� ������ ���� this�� �ٿ��ֱ⵵ �Ѵ�.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //���� �� �̵��� �Ǿ��µ� �� ������ Hierarchy�� GameMgr�� ������ ���� �ִ�.
            //�׷� ��쿣 ���� ������ ����ϴ� �ν��Ͻ��� ��� ������ִ� ��찡 ���� �� ����.
            //�׷��� �̹� ���������� instance�� �ν��Ͻ��� �����Ѵٸ� �ڽ�(���ο� ���� GameMgr)�� �������ش�.
            Destroy(this.gameObject);
        }

        GameObject clone = Instantiate(SchoolHPPrefab);
        clone.GetComponent<SchoolHP>().School = this;
    }//�ܾ�°̴ϴ�.
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
            MaxHP *= 1.1f;
            levelUp = false;
            School.stack = 0;
        }
        else if (School.stack >= 19) levelUp = true;


        //���� üũ


        HP = !levelUp ? MaxHP : MaxHP * 5;


    }
    public void GetAttack(int dmg)
    {
        HP -= dmg;

        if (HP < 0)
        {
            Money.IncreaseMoney(Random.Range(2, 4));
            if (levelUp)
            {
                Money.IncreaseMoney(Random.Range(2, 4));
                Money.IncreaseMoney(Random.Range(2, 4));
                Money.IncreaseMoney(Random.Range(2, 4));
                Money.IncreaseMoney(Random.Range(2, 4));
            }

            Dead();
        }
        //���� �ִϸ��̼� 
    }

    private void Dead()
    {
        //���� �״� ó��

        Destroy(gameObject);
    }
}
