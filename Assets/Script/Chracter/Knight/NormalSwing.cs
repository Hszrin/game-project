using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSwing : SwordSwingScript
{
    private void OnEnable()
    {
        leftAngle = 50f;
        rightAngle = 140f;
        swordDmg = 20f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player" || other.tag == "Enumy") 
            && fanShapeColCheck.SwordColCheck(other.transform, playerTransform, leftAngle, rightAngle))
        {
            //currentCol.Add(other);
            IChracterComponent colIComponent = other.transform.GetComponent<IChracterComponent>();
            ChracterComponent colComponent = colIComponent.ReturnChracterComponent();

            colComponent.HpAdjust(-Mathf.Ceil(swordDmg));
        }
    }
}
