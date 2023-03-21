using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSwing : SwordSwingScript
{
    private void OnEnable()
    {
        swordComponent.leftAngle = 50f;
        swordComponent.rightAngle = 140f;
        swordComponent.swordDmg = 20f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player" || other.tag == "Enumy") 
            && fanShapeColCheck.SwordColCheck(other.transform, playerTransform, swordComponent.leftAngle, swordComponent.rightAngle))
        {
            //currentCol.Add(other);
            IChracterComponent colIComponent = other.transform.GetComponent<IChracterComponent>();
            ChracterComponent colComponent = colIComponent.ReturnChracterComponent();

            colComponent.HpAdjust(-Mathf.Ceil(swordComponent.swordDmg));
        }
    }
}
