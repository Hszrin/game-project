using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
public interface IChracterComponent
{
    public void HpPlusTextFloating(float val);
    public void HpMinusTextFloating(float val);
    public ChracterComponent ReturnChracterComponent();
}
[Serializable]
public class ChracterComponent
{
    public GameObject chracter;
    public IChracterComponent IChracter;
    public float speed;
    public float power;
    public float maxHp;
    public float currentHp;
    public float weight;
    public float critChance;
    public Sprite chraceterImage;
    public List<string> skillList = new List<string>();
    public List<float> leftAttackAngle;
    public List<float> rightAttackAngle;
    public bool stigma;
    public Slider hpSlider;
    public Transform HPUI;
    public UnityEvent OnDestroyEvent = new UnityEvent();
    public void HpAdjust(float val)
    {
        currentHp += val;
        if(val > 0) IChracter.HpPlusTextFloating(val);
        else IChracter.HpMinusTextFloating(val);
        if (currentHp > maxHp) currentHp = maxHp;
        if (currentHp <= 0)
        {
            currentHp = 0;
            OnDestroyEvent.Invoke();
        }
        hpSlider.value = currentHp / maxHp;
    }
    public void MaxHpAdjust(float currentHp, float maxHp)
    {
        this.currentHp = currentHp;
        this.maxHp = maxHp;
        hpSlider.value = currentHp / maxHp;
    }
}
