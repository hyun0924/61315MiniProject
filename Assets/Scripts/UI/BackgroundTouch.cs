using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundTouch : MonoBehaviour
{
    [SerializeField] private WindSkill windSkill;
    Button button;

    [Header("FootPrint")]
    [SerializeField] private GameObject FootPrintPrefab;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnMouseDown);
    }

    private void OnMouseDown()
    {
        Debug.Log("touched");
        windSkill.IncreaseSkillCount();
        bool crit = Random.Range(0, 50) == 0;
        School.getInstance().GetAttack(crit ? PlayerStat.atk * 10 : PlayerStat.atk);
        //Ä¡¸íÅ¸ Ãß°¡

        // Spawn Footprint
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Instantiate(FootPrintPrefab, mousePos, Quaternion.Euler(0, 0, Random.Range(-35f, 35f)));

        // Money
        Money.IncreaseMoney(Random.Range(1, 3));

        FragmentSpawner.Instance.SpawnFragment();
    }
}
