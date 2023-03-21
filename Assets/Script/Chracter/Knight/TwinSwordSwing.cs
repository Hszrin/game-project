using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TwinSWordSwing : SwordSwingScript
{
    private SetAnimeSpriteType setSwordType;
    private void OnEnable()
    {
        leftAngle = 50;
        rightAngle = 140;
        swordDmg = ConstInt.basicSwordDamage;
        setSwordType = transform.GetComponent<SetAnimeSpriteType>();
        setSwordType.Set(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.transform.tag == "Player" || other.transform.tag == "Enumy") && fanShapeColCheck.SwordColCheck(other.transform, playerTransform, leftAngle, rightAngle))
        {
            //currentCol.Add(other);
            IChracterComponent colIComponent = other.transform.GetComponent<IChracterComponent>();
            ChracterComponent colComponent = colIComponent.ReturnChracterComponent();

            colComponent.HpAdjust(-Mathf.Ceil(swordDmg));
        }
        if ((other.transform.tag == "Player" || other.transform.tag == "Enumy") && fanShapeColCheck.SwordColCheck(other.transform, playerTransform, rightAngle, leftAngle))
        {
            //currentCol.Add(other);
            IChracterComponent colIComponent = other.transform.GetComponent<IChracterComponent>();
            ChracterComponent colComponent = colIComponent.ReturnChracterComponent();

            colComponent.HpAdjust(-Mathf.Ceil(swordDmg));
        }
    }
}
