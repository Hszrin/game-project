using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceArrowHead : MonoBehaviour
{
    private SpriteRenderer ice;
    private Shoot shoot;
    private void Start()
    {
        ice = transform.GetComponent<SpriteRenderer>();
        shoot = transform.parent.parent.GetComponent<Shoot>();
        shoot.startExitEvent.AddListener(DisableSprite);
        shoot.startShootEvent.AddListener(EnableSprite);
    }
    public void EnableSprite()
    {
        ice.enabled = true;
    }
    public void DisableSprite()
    {
        ice.enabled = false;
    }
}