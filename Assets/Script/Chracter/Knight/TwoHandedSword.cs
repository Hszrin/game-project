using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TwoHandedSword : MonoBehaviour
{
    //공격력 무게 비례 증가
    private GameObject swordSwing;
    private Knight knight;
    private ChracterComponent chracter;
    private SpriteRenderer knightSword;
    private SphereCollider sphereCollider;
    private void OnEnable()
    {
        chracter = transform.GetComponent<IChracterComponent>().ReturnChracterComponent();
        chracter.speed -= 20;
        chracter.weight += 20;

        swordSwing = transform.Find("SwordSwing").gameObject;
        swordSwing.AddComponent<TwoHandedSwordSwing>();
        Destroy(swordSwing.GetComponent<NormalSwing>());

        sphereCollider = swordSwing.GetComponent<SphereCollider>();
        sphereCollider.radius = 3.5f;

        knight = transform.GetComponent<Knight>();
        knightSword = knight.rightSword.GetComponent<SpriteRenderer>();
        knightSword.sprite = Resources.Load<Sprite>("TwoHandedSword");
    }
}