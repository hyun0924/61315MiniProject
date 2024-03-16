using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBubbleSpawn : MonoBehaviour
{
    Coroutine bossBubble;
    RectTransform rectTransform;
    private string[] bossBubbles =
    {
        "지방 방송 꺼라~", "학생이 단정해야지~", "학생의 본분은 공부!",
        "시험? 쉽게 낼게~", "첫째도 복습 둘째도 복습"
    };
    [SerializeField] private GameObject BubblePrefab;

    private void Awake()
    {
        bossBubble = null;
        rectTransform = GetComponent<RectTransform>();
    }

    public void StartSpawnBubble(BossData bossData)
    {
        bossBubble = StartCoroutine(SpawnBubble(bossData));
    }

    public void StopSpawnBubble()
    {
        StopCoroutine(bossBubble);
    }

    private IEnumerator SpawnBubble(BossData bossData)
    {
        while (true)
        {
            GameObject clone = Instantiate(BubblePrefab);

            float time = Random.Range(2f, 3f);
            int num = Random.Range(0, bossBubbles.Length + bossData.ScriptsCnt);
            string text;
            if (num >= bossBubbles.Length) text = bossData.Scripts[num - bossBubbles.Length];
            else text = bossBubbles[num];

            float width = clone.GetComponent<BossBubble>().SetText(text);
            float rangeX = (width + 100) / 2f + rectTransform.rect.x;

            Vector3 spawnPos = new Vector3(Random.Range(rangeX, -rangeX), Random.Range(-rectTransform.rect.y / 2f, rectTransform.rect.y / 2f), 0);
            clone.GetComponent<RectTransform>().anchoredPosition = spawnPos;
            clone.transform.SetParent(transform, false);

            yield return new WaitForSeconds(time);
        }
    }
}
