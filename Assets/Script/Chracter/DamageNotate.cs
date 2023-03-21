using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageNotate : MonoBehaviour
{
    private float time = 1f;
    private float x = 0;
    private float y = 0;
    Vector3 pos = new Vector3(0, 0, -1f);
    private Transform damageText;
    private void OnEnable()
    {
        damageText = transform.Find("Text");
    }

    private void Update()
    {
        x += time * Time.deltaTime;
        y = ReturnFunc(x);
        if (x >= 0.6)
        {
            Destroy(gameObject);
        }
        pos.x = x;
        pos.y = y;
        damageText.localPosition = pos;
    }

    public float ReturnFunc(float x)
    {
        return 2f - (float)Mathf.Pow(x - 1.189f, 4);
    }
}
