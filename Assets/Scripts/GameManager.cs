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
    [SerializeField] private Canvas studentCanvas;
    [SerializeField] private GameObject GUI;
    [SerializeField] private GameObject SchoolObject;
    [SerializeField] private GameObject BossScript;

    private bool isStart;
    public bool IsStart => isStart;

    private static GameManager instance;
    public static GameManager Instance => instance;
    public Canvas StudentCanvas => studentCanvas;

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
        GUI.SetActive(true);
        Money.SetMoney(0);

        SchoolObject.SetActive(true);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }

    public void BossClear()
    {
        Time.timeScale = 0;
        ClearPanel.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        ClearPanel.SetActive(false);
    }

    public void GameOver()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Retry()
    {
        GameOverPanel.SetActive(false);

        Time.timeScale = 1;
        School.getInstance().Reset();
        Money.SetMoney(0);
        WindSkill.Instance.Reset();
        ATKUPBtn.Instance.Reset();
        AddStudentBtn.Instance.Reset();

        // Destroy BossScripts
        for (int i = BossScript.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(BossScript.transform.GetChild(i).gameObject);
        }

        // Destroy BossScripts
        for (int i = studentCanvas.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(studentCanvas.transform.GetChild(i).gameObject);
        }
    }

    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
