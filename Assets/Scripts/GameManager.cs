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
    [SerializeField] private Canvas     studentCanvas;
    [SerializeField] private GameObject GUI;
    [SerializeField] private GameObject SchoolObject;

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
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
