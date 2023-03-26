using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SwordSwingScript : MonoBehaviour
{
    public Animator anime;
    public Collider col;
    public Knight knight;
    public Transform playerTransform;
    public FanShapeColCheck fanShapeColCheck = new FanShapeColCheck();
    public SwordComponent swordComponent = new SwordComponent();


    private void Awake()
    {
        playerTransform = transform.parent;
        knight = playerTransform.GetComponent<Knight>();
        swordComponent = knight.swordComponent;
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

}

[Serializable]
public class SwordComponent
{
    public float swordDmg;
    public float leftAngle;
    public float rightAngle;
}