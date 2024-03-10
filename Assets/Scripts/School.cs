using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class School : MonoBehaviour
{
    protected float HP;
    private float MaxHP;
    public float initialHP;
    protected static School instance = null;
    public static int stack; // nn번째 격파에 사용
    private bool isBoss;
    public bool IsBoss => isBoss;
    private int bossType;
    private int BossMoney;
    Coroutine shake;
    Coroutine bossScript;
    Rigidbody2D rb;
    SpriteRenderer sr;
    BoxCollider2D boxCollider;
    AudioSource audioSource;

    private string[] bossScripts =
    {
        "지방 방송 꺼라~", "학생이 단정해야지~", "학생의 본분은 공부!",
        "시험? 쉽게 낼게~", "첫째도 복습 둘째도 복습"
    };
    [SerializeField] private int bossPeriod;

    [Header("UI")]
    [SerializeField] private Image SchoolHPBar;
    [SerializeField] private TextMeshProUGUI SchoolHPText;
    [SerializeField] private TextMeshProUGUI SchoolNameText;
    [SerializeField] private GameObject BossAlertLine;
    [SerializeField] private GameObject BubblePrefab;

    [Header("Speed")]
    [SerializeField] private float SchoolSpeed;
    [SerializeField] private float BossSpeed;
    private float currentSpeed;

    [Header("Shake")]
    [SerializeField] private float shakeTime;
    [SerializeField] private float shakeIntensity;

    [Header("BreakStage")]
    [SerializeField] private Sprite[] BreakStages;
    [SerializeField] private BossData[] bossData;

    void Awake()
    {
        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;
            isBoss = false;
            School.stack = 0;
            MaxHP = initialHP;

            rb = GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
            boxCollider = GetComponent<BoxCollider2D>();
            audioSource = GetComponent<AudioSource>();

            shake = null;
            bossScript = null;
            //씬 전환이 되더라도 파괴되지 않게 한다.
            //gameObject만으로도 이 스크립트가 컴포넌트로서 붙어있는 Hierarchy상의 게임오브젝트라는 뜻이지만, 
            //나는 헷갈림 방지를 위해 this를 붙여주기도 한다.
            // DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 GameMgr이 존재할 수도 있다.
            //그럴 경우엔 이전 씬에서 사용하던 인스턴스를 계속 사용해주는 경우가 많은 것 같다.
            //그래서 이미 전역변수인 instance에 인스턴스가 존재한다면 자신(새로운 씬의 GameMgr)을 삭제해준다.
            Destroy(this.gameObject);
        }

        // Resolution
        if (Screen.width < Screen.height)
        {
            float width = Camera.main.orthographicSize * 1.88f * Screen.width / Screen.height / 10.0f;
            transform.localScale = new Vector3(width, width, width);
        }
        BossMoney = 10;
    }

    public static School getInstance()
    {
        return instance;
    }

    protected void Start()
    {
        ReGen();
    }

    private void OnEnable()
    {
        if (isBoss)
        {
            bossScript = StartCoroutine(BossScript());
        }
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.y) >= 7.3f)
        {
            transform.position = new Vector3(0, 7.3f);
            rb.velocity = Vector3.zero;
        }
    }

    public void ReGen()
    {
        // Stop Coroutines
        if (shake != null)
        {
            StopCoroutine(shake);
            shake = null;
        }
        if (bossScript != null)
        {
            StopCoroutine(bossScript);
            bossScript = null;
        }

        stack++;
        SchoolHPBar.fillAmount = 1;
        rb.velocity = Vector2.zero;
        transform.position = new Vector3(0, 7.3f);
        isBoss = false;

        // bossPeriod번째마다 보스 체크
        if (stack % bossPeriod == 0)
        {
            BossAlertLine.SetActive(true);
            currentSpeed = BossSpeed;
            HP = MaxHP * 5;
            isBoss = true;
            bossType = Random.Range(0, bossData.Length);
            SchoolNameText.text = bossData[bossType].Name + " 학교";
            sr.sprite = bossData[bossType].Stages[0];
        }
        else
        {
            gameObject.SetActive(true);
            currentSpeed = SchoolSpeed;
            SchoolNameText.text = stack + "번째 학교";
            HP = MaxHP;
            sr.sprite = BreakStages[0];
        }

        SchoolHPText.text = (int)HP + "/" + (int)HP;
        rb.gravityScale = 0.35f * currentSpeed;
    }

    public void Reset()
    {
        stack = 0;
        MaxHP = initialHP;
        SchoolSpeed /= BossSpeed;
        BossSpeed = 1;

        ReGen();
    }

    private void NextPhase()
    {
        MaxHP *= 1.1f;          // HP +1%
        SchoolSpeed *= 1.03f;   // speed +3%
        BossSpeed *= 1.03f;     // speed +3%

        Money.IncreaseMoney(BossMoney);
        BossMoney += 5;
    }

    private void GetAttack(float dmg)
    {
        HP -= dmg;
        if (isBoss)
        {
            SchoolHPBar.fillAmount = HP / (MaxHP * 5);
            SchoolHPText.text = (int)Mathf.Max(HP, 0) + "/" + (int)(MaxHP * 5);
        }
        else
        {
            SchoolHPBar.fillAmount = HP / MaxHP;
            SchoolHPText.text = (int)Mathf.Max(HP, 0) + "/" + (int)MaxHP;
        }
        Debug.Log("Attacked!" + dmg + "damge");

        // Change Sprite
        if (HP <= 0)
        {
            Dead();
            return;
        }
        float unit = 1f / BreakStages.Length;
        int stage = (int)(SchoolHPBar.fillAmount / unit);
        if (isBoss)
        {
            sr.sprite = bossData[bossType].Stages[BreakStages.Length - stage - 1];
        }
        else
        {
            sr.sprite = BreakStages[BreakStages.Length - stage - 1];
        }
        boxCollider.size = sr.sprite.bounds.size;
    }

    public void GetAttackByPlayer(float dmg)
    {
        GetAttack(dmg);

        rb.velocity = Vector3.zero;

        if (gameObject.activeSelf)
        {
            shake = StartCoroutine(Shake());
        }
    }

    public void GetAttackByWind(float dmg)
    {
        rb.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
        audioSource.Play();
        GetAttack(dmg);
    }

    public void GetAttackByStudent(float dmg)
    {
        GetAttack(dmg);
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

        transform.position = new Vector3(0, startPosition.y - currentSpeed * shakeTime, 0);
    }

    private IEnumerator BossScript()
    {
        GameObject bossScriptCanvas = GameObject.FindWithTag("BossScript").gameObject;
        RectTransform rt = bossScriptCanvas.GetComponent<RectTransform>();
        Vector3 spawnPos = GetBottomLeftCorner(rt);

        while (true)
        {
            spawnPos -= new Vector3(Random.Range(-rt.rect.x / 2f, rt.rect.x / 2f), Random.Range(-rt.rect.y / 2f, rt.rect.y / 2f), 0);
            GameObject clone = Instantiate(BubblePrefab, spawnPos, Quaternion.identity);
            float time = Random.Range(2f, 3f);
            int num = Random.Range(0, bossScripts.Length + bossData[bossType].ScriptsCnt);

            clone.transform.SetParent(bossScriptCanvas.transform, false);
            string text;
            if (num >= bossScripts.Length) text = bossData[bossType].Scripts[num - bossScripts.Length];
            else text = bossScripts[num];
            clone.GetComponent<BossBubble>().SetText(text);
            yield return new WaitForSeconds(time);
        }
    }

    Vector3 GetBottomLeftCorner(RectTransform rt)
    {
        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);
        return v[0];
    }

    private void Dead()
    {
        // 대충 죽는 처리
        if (shake != null)
        {
            StopCoroutine(shake);
            shake = null;
        }
        if (bossScript != null)
        {
            StopCoroutine(bossScript);
            bossScript = null;
            GameManager.Instance.DestroyBossScripts();
        }

        gameObject.SetActive(false);
        if (isBoss)
        {
            GameManager.Instance.BossClear();
            NextPhase();
        }
        
        ReGen();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "GameOver")
        {
            if (shake != null)
            {
                StopCoroutine(shake);
                shake = null;
            }
            GameManager.Instance.GameOver();
        }
    }
}
