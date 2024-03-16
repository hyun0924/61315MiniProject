using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonArrow : MonoBehaviour
{
    RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        StartCoroutine(ChangePos());
    }

    private IEnumerator ChangePos()
    {
        float posY = 410f;
        float destY = 395f;
        float speed = 8;
        float posX = rectTransform.anchoredPosition.x;

        while (true)
        {
            rectTransform.anchoredPosition = new Vector3(posX, posY);
            posY = Mathf.Lerp(posY, destY, speed * Time.unscaledDeltaTime);

            if (Mathf.Abs(posY - destY) <= 0.1f)
            {
                destY += 2 * (402.5f - destY);
                speed += 2 * (12f - speed);
            }
            yield return null;
        }
    }
}
