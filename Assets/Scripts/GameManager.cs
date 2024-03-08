using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject GameStartPanel;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject ClearPanel;
    [SerializeField] private GameObject TouchPanel;
    [SerializeField] private GameObject studentDamageTextSpawner;
    [SerializeField] private GameObject GUI;
    [SerializeField] private GameObject SchoolObject;
    [SerializeField] private GameObject BossScript;

    private bool isStart;
    public bool IsStart => isStart;

    private static GameManager instance;
    public static GameManager Instance => instance;
    public GameObject StudentDamageTextSpawner => studentDamageTextSpawner;

    private void Awake()
    {
        instance = this;
        isStart = false;
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
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
        Time.timeScale = 0;
    }

    public void Retry()
    {
        TouchPanel.SetActive(true);
        GameOverPanel.SetActive(false);
        School.getInstance().Reset();
        Money.SetMoney(0);
        WindSkill.Instance.Reset();
        ATKUPBtn.Instance.Reset();
        AddStudentBtn.Instance.Reset();
        DestroyBossScripts();

        // Destroy StudentDamageText
        for (int i = studentDamageTextSpawner.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(studentDamageTextSpawner.transform.GetChild(i).gameObject);
        }

        Time.timeScale = 1;
    }

    public void DestroyBossScripts()
    {
        for (int i = BossScript.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(BossScript.transform.GetChild(i).gameObject);
        }
    }

    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
