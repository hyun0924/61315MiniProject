using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindScript : MonoBehaviour
{

    private void Awake()
    {
        StartCoroutine("ToTrigger");
    }

    IEnumerator ToTrigger()
    {
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void Update()
    {
        gameObject.transform.Translate(Vector3.up * 15f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "School")
        {
            School.getInstance().GetAttackByWind(PlayerStat.atk * 5);
            //WindSkill.usingWind = false;
            Destroy(gameObject);
        }
    }
}
