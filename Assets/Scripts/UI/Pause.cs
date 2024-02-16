using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            PausePanel.SetActive(true);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }
}
