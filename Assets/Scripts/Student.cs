using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : MonoBehaviour
{
    SpriteRenderer sr;
    public static float atk;
    public static int studentNum;
    public static int gap;
    private Canvas canvas;
    [SerializeField] private GameObject DamageTextPrefab;
    
    private void Awake()
    {
        canvas = GameManager.Instance.StudentCanvas;
        sr = GetComponent<SpriteRenderer>();
        if (transform.position.x > 0) sr.flipX = true;

        // StartCoroutine("attack", 5);
        studentNum++;
        if (studentNum > 10)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gap = 1;
        AtkChange();
    }

    public static void AtkChange()
    {
        atk = (int) (PlayerStat.atk / 2f);
    }

    // Animation Event
    public void Attack()
    {
        if (!School.getInstance().gameObject.activeSelf) return;

        School.getInstance().GetAttackByStudent(atk);

        // Show Damage
        Vector3 Position = transform.position + new Vector3(0.28f, 0.28f);
        GameObject clone = Instantiate(DamageTextPrefab, Position, Quaternion.identity);
        clone.transform.SetParent(canvas.transform);
        clone.transform.localScale = Vector3.one;
        clone.GetComponent<DamageText>().SetText(atk, 0.2f);
    }
}
