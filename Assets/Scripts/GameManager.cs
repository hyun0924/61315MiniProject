using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject GameStartPanel;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private TextMeshProUGUI ResultText;
    [SerializeField] private GameObject ClearPanel;
    [SerializeField] private GameObject TouchPanel;
    [SerializeField] private GameObject GUI;
    [SerializeField] private GameObject SchoolObject;
    [SerializeField] private GameObject[] BossBubbleContainers;

    [Header("Container")]
    [SerializeField] private GameObject studentDamageTextContainer;
    [SerializeField] private GameObject FragmentContainer;
    [SerializeField] private GameObject FootprintContainer;
    [SerializeField] private GameObject TitleFriends;

    private bool isStart;
    public bool IsStart => isStart;
    private int ClickCount;

    private static GameManager instance;
    public static GameManager Instance => instance;
    public GameObject StudentDamageTextContainer => studentDamageTextContainer;
    AudioSource gameStartAudio;

    GameManager()
    {
        instance = this;
    }

    private void Awake()
    {
        isStart = false;
        Time.timeScale = 1;
        ClickCount = 0;
        gameStartAudio = GetComponent<AudioSource>();

        ActiveFriendsRandom();
    }

    private void ActiveFriendsRandom()
    {
        for (int i = 0; i < TitleFriends.transform.childCount; i++)
        {
            if (isStart) break;
            GameObject friend = TitleFriends.transform.GetChild(i).gameObject;
            friend.GetComponent<Animator>().Play("Friend", -1, Random.Range(0f, 1f));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isStart) Pause();
            else
            {
                if (ClickCount < 1)
                {
                    ClickCount++;
                    if (!IsInvoking("ResetEsc"))
                    {
                        Invoke("ResetEsc", 1.0f);
                        ShowAndroidToastMessage("\'뒤로\'버튼을 한번 더 누르시면 종료됩니다.");
                    }
                }
                else
                {
                    CancelInvoke("ResetEsc");
                    Application.Quit();
                }
            }
        }
    }

    private void ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }

    void ResetEsc()
    {
        ClickCount = 0;
    }

    public void GameStart()
    {
        isStart = true;
        GameStartPanel.SetActive(false);
        TouchPanel.SetActive(true);
        GUI.SetActive(true);

        Money.SetMoney(0);
        SchoolObject.SetActive(true);
        Time.timeScale = 1;
        gameStartAudio.Play();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        TouchPanel.SetActive(false);
        PausePanel.SetActive(true);
    }

    public void BossClear()
    {
        Time.timeScale = 0;
        TouchPanel.SetActive(false);
        ClearPanel.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        TouchPanel.SetActive(true);
        PausePanel.SetActive(false);
        ClearPanel.SetActive(false);
    }

    public void GameOver()
    {
        TouchPanel.SetActive(false);
        GameOverPanel.SetActive(true);
        ResultText.text = "학교\n" + (School.stack - 1) + "개 격파!";
        Time.timeScale = 0;
    }

    public void Retry()
    {
        TouchPanel.SetActive(true);
        GameOverPanel.SetActive(false);
        PausePanel.SetActive(false);

        School.getInstance().Reset();
        Money.SetMoney(0);
        WindSkill.Instance.Reset();
        ATKUPBtn.Instance.Reset();
        AddStudentBtn.Instance.Reset();
        BurningGauge.Instance.Reset();

        DestroyBossBubbles();
        DestroyFragments();
        DestroyFootprints();

        // Destroy StudentDamageText
        for (int i = studentDamageTextContainer.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(studentDamageTextContainer.transform.GetChild(i).gameObject);
        }

        Time.timeScale = 1;
    }

    public void DestroyBossBubbles()
    {
        for (int i = 0; i < BossBubbleContainers.Length; i++)
        {
            for (int j = BossBubbleContainers[i].transform.childCount - 1; j >= 0; j--)
            {
                Destroy(BossBubbleContainers[i].transform.GetChild(j).gameObject);
            }
        }
    }

    private void DestroyFragments()
    {
        for (int i = FragmentContainer.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(FragmentContainer.transform.GetChild(i).gameObject);
        }
    }

    private void DestroyFootprints()
    {
        for (int i = FootprintContainer.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(FootprintContainer.transform.GetChild(i).gameObject);
        }
    }

    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
