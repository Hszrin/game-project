using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TwinSWordSwing : SwordSwingScript
{
    private SetAnimeSpriteType setSwordType;
    private void OnEnable()
    {
        swordComponent.leftAngle = 50;
        swordComponent.rightAngle = 140;
        swordComponent.swordDmg = ConstInt.basicSwordDamage;
        setSwordType = transform.GetComponent<SetAnimeSpriteType>();
        setSwordType.Set(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.transform.tag == "Player" || other.transform.tag == "Enumy") && fanShapeColCheck.SwordColCheck(other.transform, playerTransform, swordComponent.leftAngle, swordComponent.rightAngle))
        {
            //currentCol.Add(other);
            IChracterComponent colIComponent = other.transform.GetComponent<IChracterComponent>();
            ChracterComponent colComponent = colIComponent.ReturnChracterComponent();

            colComponent.HpAdjust(-Mathf.Ceil(swordComponent.swordDmg));
        }
        if ((other.transform.tag == "Player" || other.transform.tag == "Enumy") && fanShapeColCheck.SwordColCheck(other.transform, playerTransform, swordComponent.rightAngle, swordComponent.leftAngle))
        {
            //currentCol.Add(other);
            IChracterComponent colIComponent = other.transform.GetComponent<IChracterComponent>();
            ChracterComponent colComponent = colIComponent.ReturnChracterComponent();

            colComponent.HpAdjust(-Mathf.Ceil(swordComponent.swordDmg));
        }
    }
}
