using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SchoolHP : MonoBehaviour
{
    private School school;
    public School School
    {
        set => school = value;
        get => school;
    }

    private Slider slider;
    [SerializeField] private Vector3 distance;
    private GameObject canvas;

    RectTransform rectTransform;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        rectTransform = GetComponent<RectTransform>();
        rectTransform.SetParent(canvas.transform);
    }

    private void Update()
    {
        if (school != null)
        {
            slider.value = school.SchoolHP / school.MaxHP;
        }
    }

    private void LateUpdate()
    {
        if (school == null)
        {
            Destroy(gameObject);
            return;
        }

        rectTransform.position = Camera.main.WorldToScreenPoint(school.transform.position) + distance;
    }
}
