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
    public static int stack; // nn번째 격파에 사용
    private bool isBoss;
    Rigidbody2D rb;
    SpriteRenderer sr;

    [SerializeField] private int bossPeriod;

    [Header("UI")]
    [SerializeField] private Image SchoolHPBar;
    [SerializeField] private TextMeshProUGUI SchoolNameText;

    [Header("Speed")]
    [SerializeField] private float SchoolSpeed;
    [SerializeField] private float BossSpeed;
    private float currentSpeed;

    [Header("Shake")]
    [SerializeField] private float shakeTime;
    [SerializeField] private float shakeIntensity;

    [Header("BreakStage")]
    [SerializeField] private Sprite[] BreakStages;

    void Awake()
    {
        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;
            isBoss = false;
            School.stack = 0;
            rb = GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
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
        ReGen();
    }

    private void Update()
    {
        // transform.position += Vector3.down * currentSpeed * Time.deltaTime;
    }

    public void ReGen()
    {
        stack++;
        SchoolHPBar.fillAmount = 1;
        transform.position = new Vector3(0, 7.5f);
        gameObject.SetActive(true);
        isBoss = false;
        sr.sprite = BreakStages[0];

        // bossPeriod번째마다 보스 체크
        if (stack % bossPeriod == 0)
        {
            currentSpeed = BossSpeed;
            SchoolNameText.text = "Boss";
            HP = MaxHP * 5;
            isBoss = true;
        }
        else
        {
            currentSpeed = SchoolSpeed;
            SchoolNameText.text = "";
            HP = MaxHP;
        }
    }

    private void NextPhase()
    {
        MaxHP *= 1.1f;
        SchoolSpeed *= 1.05f; // 임의로 speed +5%
    }

    public void GetAttack(int dmg)
    {
        HP -= dmg;
        if (isBoss) SchoolHPBar.fillAmount = HP / (MaxHP * 5);
        else SchoolHPBar.fillAmount = HP / MaxHP;
        Debug.Log("Attacked!" + dmg + "damge");

        StopCoroutine(Shake());
        StartCoroutine(Shake());

        rb.velocity = Vector3.zero;

        // Break stage
        switch (SchoolHPBar.fillAmount)
        {
            case float hp when hp <= 0 :
                Dead();
                break;
            case float hp when hp <= 0.2f :
                sr.sprite = BreakStages[4];
                break;
            case float hp when hp <= 0.4f :
                sr.sprite = BreakStages[3];
                break;
            case float hp when hp <= 0.6f :
                sr.sprite = BreakStages[2];
                break;
            case float hp when hp <= 0.8f :
                sr.sprite = BreakStages[1];
                break;
        }
    }

    private IEnumerator Shake()
    {
        Vector3 startPosition = transform.position;
        float time = shakeTime;

        while (time > 0.0f)
        {
            time -= Time.deltaTime;
            transform.position = startPosition + Random.insideUnitSphere * shakeIntensity;

            yield return null;
        }

        transform.position = startPosition + Vector3.down * currentSpeed * shakeTime;
    }

    private void Dead()
    {
        //대충 죽는 처리
        gameObject.SetActive(false);

        // 보스 잡으면 NextPhase
        if (isBoss) NextPhase();

        ReGen();
    }
}
