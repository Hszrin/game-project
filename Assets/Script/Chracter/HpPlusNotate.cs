using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpPlusNotate : MonoBehaviour
{
    private float time = 1f;
    private float y = 0;
    Vector3 pos = new Vector3(0, 0, -1f);
    private Transform damageText;
    private void OnEnable()
    {
        damageText = transform.Find("Text");
    }

    private void Update()
    {
        y += time * Time.deltaTime;
        pos.y = y;
        if (y >= 0.6)
        {
            Destroy(gameObject);
        }
        damageText.localPosition = pos;
    }
}
