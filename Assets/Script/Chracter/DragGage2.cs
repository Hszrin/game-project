using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragGage2 : MonoBehaviour
{
    public Slider slider;
    public float flag;

    private void Start()
    {
        flag = 1;
        slider = transform.GetComponent<Slider>();
    }

    private void Update()
    {
        slider.value += Time.deltaTime * flag;
        if (slider.value == 1 || slider.value == 0) flag *= -1;
    }
}
