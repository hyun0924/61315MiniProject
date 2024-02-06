using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentSpawner : MonoBehaviour
{
    private static FragmentSpawner instance;
    public static FragmentSpawner Instance => instance;

    BoxCollider2D boxCollider2D;

    [Header("Fragment")]
    [SerializeField] private GameObject MoneyPrefab;
    [SerializeField] private GameObject FragmentPrefab;

    public FragmentSpawner()
    {
        instance = this;
    }

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void SpawnFragment()
    {
        Instantiate(MoneyPrefab, RandomPosition(), Quaternion.Euler(0, 0, Random.Range(0, 360)));
        Instantiate(FragmentPrefab, RandomPosition(), Quaternion.Euler(0, 0, Random.Range(0, 360)));
        Instantiate(FragmentPrefab, RandomPosition(), Quaternion.Euler(0, 0, Random.Range(0, 360)));
    }

    private Vector3 RandomPosition()
    {
        Vector3 originPos = transform.position;

        float rangeX = boxCollider2D.bounds.size.x;
        float rangeY = boxCollider2D.bounds.size.y;

        float x = Random.Range(-1 * (rangeX / 2.0f), rangeX / 2.0f);
        float y = Random.Range(-1 * (rangeY / 2.0f), rangeY / 2.0f);

        return originPos + new Vector3(x, y);
    }
}
