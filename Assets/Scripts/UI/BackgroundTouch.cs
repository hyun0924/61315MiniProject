using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundTouch : MonoBehaviour
{
    [SerializeField] private WindSkill windSkill;
    Button button;

    [SerializeField] private GameObject FootPrintPrefab;
    [SerializeField] private GameObject DamageTextPrefab;

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
        
        // Show Damage
        Vector3 textPos = Input.mousePosition + Vector3.up * 100f;
        GameObject clone = Instantiate(DamageTextPrefab, textPos, Quaternion.identity);
        clone.transform.SetParent(transform);

        // Money
        Money.IncreaseMoney(Random.Range(1, 3));

        FragmentSpawner.Instance.SpawnFragment();
    }
}
