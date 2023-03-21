using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChracterProfileStatusHp : MonoBehaviour
{
    public Slider Hp;

    private void OnEnable()
    {
        Hp = transform.GetComponent<Slider>();
    }
}
