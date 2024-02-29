using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject ClearPanel;
    [SerializeField] private Canvas studentCanvas;

    private static GameManager instance;
    public static GameManager Instance => instance;
    public Canvas StudentCanvas => studentCanvas;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void Pause()
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
