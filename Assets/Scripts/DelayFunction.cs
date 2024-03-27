using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum ButtonType
{
    Resume, Retry, Exit
}

public class DelayFunction : MonoBehaviour
{
    [SerializeField] ButtonType buttonType;
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.RemoveAllListeners();
        StartCoroutine(EnableAction());
    }

    private IEnumerator EnableAction()
    {
        yield return new WaitForSecondsRealtime(2f);

        if (buttonType == ButtonType.Resume)
            button.onClick.AddListener(GameManager.Instance.Resume);
        else if (buttonType == ButtonType.Retry)
            button.onClick.AddListener(GameManager.Instance.Retry);
        else if (buttonType == ButtonType.Exit)
            button.onClick.AddListener(GameManager.Instance.Exit);

    }
}
