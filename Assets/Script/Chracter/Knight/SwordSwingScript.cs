using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ISwordComponent
{
    public SwordComponent ReturnSwordComponent();
}
public class SwordSwingScript : MonoBehaviour, ISwordComponent
{
    public Animator anime;
    public Collider col;
    public Knight knight;
    public Transform playerTransform;
    public FanShapeColCheck fanShapeColCheck = new FanShapeColCheck();
    public SwordComponent swordComponent = new SwordComponent();

    public Dictionary<string, float> swordAdditionalDamage = new Dictionary<string, float>();

    private void Awake()
    {
        playerTransform = transform.parent;
        playerTransform.GetComponent<Player>().playerSpecialStats.AddSpecialList("검 공격력", swordComponent.swordDmg);
        knight = transform.parent.GetComponent<Knight>();
        knight.swing.AddListener(Swing);
        knight.swingReset.AddListener(ResetSwing);
        col = transform.GetComponent<Collider>();
        anime = transform.GetComponent<Animator>();
    }
    public void Swing()
    {
        col.enabled = true;
        anime.SetBool("IsSwing", true);
        knight.onDeActivateLeftSword.Invoke();
        knight.onDeActivateRightSword.Invoke();
    }

    public void ResetSwing()
    {
        anime.SetBool("IsSwing", false);
        col.enabled = false;
    }

    public SwordComponent ReturnSwordComponent()
    {
        return swordComponent;
    }
}

[Serializable]
public class SwordComponent
{
    public float swordDmg;
    public float leftAngle;
    public float rightAngle;
}