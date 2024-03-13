using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    [SerializeField] private AudioClip normalBGM;
    [SerializeField] private AudioClip burningBGM;
    private bool isBurning;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        isBurning = false;
    }

    private void LateUpdate()
    {
        if (BurningGauge.IsBurning && !isBurning)
        {
            isBurning = true;
            audioSource.clip = burningBGM;
            audioSource.Play();
        }
        else if (!BurningGauge.IsBurning && isBurning)
        {
            isBurning = false;
            audioSource.clip = normalBGM;
            audioSource.Play();
        }
    }

    public void Pause()
    {
        audioSource.Pause();
    }

    public void Resume()
    {
        audioSource.UnPause();
    }

}
