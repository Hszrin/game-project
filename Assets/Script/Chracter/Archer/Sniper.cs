using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Archer
{
    private void Start()
    {
        Destroy(gameObject.GetComponent<NormalMode>());
        AfterStart();
        arrowPrefab = Resources.Load<GameObject>("SniperArrow");
    }
    public ArrowComponent arrowComponent;
    public override void ShootArrow(Vector3 arrowDirection, Quaternion transformRotation)
    {
        playerRigid.velocity = transform.forward * player.gage.value * arrowDmg / 2;
        arrow = Instantiate(arrowPrefab, transform.position, transformRotation);
        arrowComponent = arrow.GetComponent<IArrow>().ReturnArrowComponent();
        arrow.GetComponent<Rigidbody>().velocity = -arrow.transform.forward * player.gage.value * arrowDmg * 2;
        arrowComponent.onDestroy.AddListener(turn.SubMovingCnt);
        arrowComponent.archerPos = transform.position;
        arrowComponent.dmg = arrowDmg * player.gage.value;
        arrow.GetComponent<CheckOnGround>().outBorder.AddListener(turn.SubMovingCnt);
        turn.AddMovingCnt();
    }
}
