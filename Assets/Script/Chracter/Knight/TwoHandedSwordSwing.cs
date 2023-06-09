using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TwoHandedSwordSwing : SwordSwingScript
{
    private SetAnimeSpriteType setSwordType;
    private ChracterComponent chracter;
    private void OnEnable()
    {
        chracter = playerTransform.GetComponent<IChracterComponent>().ReturnChracterComponent();

        swordComponent.leftAngle = 50;
        swordComponent.rightAngle = 140;
        swordComponent.swordDmg = (100 + chracter.weight)/100*ConstInt.basicSwordDamage;
        setSwordType = transform.GetComponent<SetAnimeSpriteType>();
        setSwordType.Set(0);
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
    }

}
