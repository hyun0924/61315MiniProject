using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundTouch : MonoBehaviour
{
    [SerializeField] private WindSkill windSkill;
    Button button;

    [SerializeField] private GameObject BurningFootPrintPrefab;
    [SerializeField] private GameObject FootPrintPrefab;
    [SerializeField] private GameObject FootPrintContainer;

    [Header("Sounds")]
    [SerializeField] private AudioClip[] AttackSounds;
    [SerializeField] private AudioClip CriticalSound;
    RectTransform rectTransform;
    AudioSource audioSource;
    private float nextClick;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnMouseDown);
        rectTransform = GetComponent<RectTransform>();
        audioSource = GetComponent<AudioSource>();
        nextClick = 0.15f;
    }

    private void Update()
    {
        // For Mobile Touch
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Mathf.Min(1, Input.touchCount); i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    if (Screen.height - touch.position.y >= 400f && touch.position.y >= 400f)
                        BreakSchool(touch.position);
                }
            }
        }

        // Auto Click
        if (BurningGauge.IsBurning)
        {
            nextClick -= Time.deltaTime;
            if (nextClick <= 0)
            {
                RandomBreakSchool();
                nextClick = 0.15f;
            }
        }
        else nextClick = 0.15f;
    }

    // For PC Click
    private void OnMouseDown()
    {
        // BreakSchool(Input.mousePosition);
    }

    private void BreakSchool(Vector3 pos)
    {
        if (!School.getInstance().gameObject.activeSelf) return;

        windSkill.IncreaseSkillCount();
        bool crit = Random.Range(0, 100) == 0;
        School.getInstance().GetAttackByPlayer(crit ? PlayerStat.atk * 10 : PlayerStat.atk);
        BurningGauge.Instance.ChargeBurningPower();
        //Ä¡¸íÅ¸ Ãß°¡

        SpawnFootprint(crit, pos, FootPrintPrefab);

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

    private void SpawnFootprint(bool crit, Vector3 pos, GameObject prefab)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(pos);
        mousePos.z = 0;
        GameObject footprint = Instantiate(prefab, mousePos, Quaternion.identity);
        footprint.transform.localScale = Vector3.one;
        footprint.GetComponentInChildren<Footprint>().SetDamageText(crit);
        footprint.transform.SetParent(FootPrintContainer.transform);
        if (crit) footprint.GetComponentInChildren<Footprint>().CriticalSize();
    }

    public void RandomBreakSchool()
    {
        if (!School.getInstance().gameObject.activeSelf) return;

        School.getInstance().GetAttackByPlayer(PlayerStat.atk);

        Vector3 randomPos = new Vector3(Random.Range(0, Screen.width), Random.Range(400f, Screen.height - 400f));
        SpawnFootprint(false, randomPos, BurningFootPrintPrefab);

        int randomSound = Random.Range(0, AttackSounds.Length);
        audioSource.clip = AttackSounds[randomSound];
        audioSource.Play();

        // Money
        Money.IncreaseMoney(Random.Range(1, 3));

        FragmentSpawner.Instance.SpawnFragment();
    }
}
