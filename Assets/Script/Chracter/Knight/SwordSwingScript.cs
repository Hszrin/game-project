using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwordSwingScript : MonoBehaviour
{
    public Animator anime;
    public Collider col;
    public float swordDmg;
    public float leftAngle;
    public float rightAngle;
    public Knight knight;
    public Transform playerTransform;
    public FanShapeColCheck fanShapeColCheck = new FanShapeColCheck();

    public Dictionary<string, float> swordAdditionalDamage = new Dictionary<string, float>();

    private void Awake()
    {
        playerTransform = transform.parent;
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
}

