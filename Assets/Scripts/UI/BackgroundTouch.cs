using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundTouch : MonoBehaviour
{
    [SerializeField] private WindSkill windSkill;
    Button button;

    [SerializeField] private GameObject FootPrintPrefab;
    [SerializeField] private GameObject FootPrintContainer;

    [Header("Sounds")]
    [SerializeField] private AudioClip[] AttackSounds;
    [SerializeField] private AudioClip CriticalSound;
    AudioSource audioSource;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnMouseDown);
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        if (!School.getInstance().gameObject.activeSelf) return;

        windSkill.IncreaseSkillCount();
        bool crit = Random.Range(0, 100) == 0;
        School.getInstance().GetAttackByPlayer(crit ? PlayerStat.atk * 10 : PlayerStat.atk);
        //Ä¡¸íÅ¸ Ãß°¡

        // Spawn Footprint
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        GameObject footprint = Instantiate(FootPrintPrefab, mousePos, Quaternion.identity);
        footprint.transform.localScale = Vector3.one;
        footprint.GetComponentInChildren<Footprint>().SetDamageText(crit);
        footprint.transform.SetParent(FootPrintContainer.transform);
        if (crit) footprint.GetComponentInChildren<Footprint>().CriticalSize();

        // Sounds
        if (crit)
        {
            audioSource.clip = CriticalSound;
        }
        else
        {
            int randomSound = Random.Range(0, AttackSounds.Length);
            audioSource.clip = AttackSounds[randomSound];
        }
        audioSource.Play();

        // Money
        Money.IncreaseMoney(Random.Range(1, 3));

        FragmentSpawner.Instance.SpawnFragment();
    }
}
