using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(3f, 5f));
    }

    private void Update()
    {
        // 화면 밖으로 나가면 삭제
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
}
