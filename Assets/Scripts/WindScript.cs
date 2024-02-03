using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindScript : MonoBehaviour
{

    private void Awake()
    {
        StartCoroutine("ToTrigger");
        StartCoroutine("MoveForwardAndDestroy");
    }

    IEnumerator ToTrigger()
    {

        
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<BoxCollider2D>().isTrigger=true;
       
    }
    private IEnumerator MoveForwardAndDestroy()
    {
        float duration = 3f;
        yield return new WaitForSeconds(duration);
        // 3�� �Ŀ� �ڱ� �ڽ� �ı�
        Destroy(gameObject);
    }
    private void Update()
    {
        gameObject.transform.Translate(Vector3.up*0.05f);
    }

}
