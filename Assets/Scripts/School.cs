using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class School : MonoBehaviour
{
    protected float HP;
    public float MaxHP;
    protected static School instance = null;
    public static bool levelUp;
    public static int stack; // nn��° ���Ŀ� ���

    [SerializeField] private int bossPeriod;

    [SerializeField] private Slider SchoolHPSlider;
    [SerializeField] private TextMeshProUGUI SchoolNameText;

    [SerializeField] private float SchoolSpeed;
    [SerializeField] private float BossSpeed;
    private float currentSpeed;

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
    }//�ܾ�°̴ϴ�.

    public static School getInstance()
    {
        return instance;
    }

    protected void Start()
    {
        ReGen();
    }

    private void Update()
    {
        transform.position += Vector3.down * currentSpeed * Time.deltaTime;
    }

    public void ReGen()
    {
        stack++;
        SchoolHPSlider.value = 1;
        transform.position = new Vector3(0, 7.5f);
        gameObject.SetActive(true);

        // bossPeriod��°���� ���� üũ
        if (stack % bossPeriod == 0)
        {
            currentSpeed = BossSpeed;
            SchoolNameText.text = "Boss";
            HP = MaxHP * 5;
        }
        else
        {
            currentSpeed = SchoolSpeed;
            SchoolNameText.text = "";
            HP = MaxHP;
        }
    }

    private void LevelUP()
    {
        MaxHP *= 1.1f;
        SchoolSpeed *= 1.05f; // ���Ƿ� speed +5%
    }

    public void GetAttack(int dmg)
    {
        HP -= dmg;
        SchoolHPSlider.value = HP / MaxHP;

        if (HP <= 0)
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
        gameObject.SetActive(false);

        // ���� ������ LevelUp
        if (stack % bossPeriod == 0) LevelUP();

        ReGen();
    }
}
