using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polydactyly : Archer
{
    private Vector3 polydactylyArrowRight = new Vector3(0, 15, 0);
    private Vector3 polydactylyArrowLeft = new Vector3(0, -15, 0);
    private void Start()
    {
        Destroy(gameObject.GetComponent<NormalMode>());
        AfterStart();
        setArrowType.Set(0);
    }

    public ArrowComponent arrowComponent;
    public void ShootPolydactyly(Vector3 arrowDirection, Quaternion transformRotation)
    {
        playerRigid.velocity = transform.forward * player.gage.value * ConstInt.basicDamage / 2;
        arrow = Instantiate(arrowPrefab, transform.position, transformRotation);
        arrowComponent = arrow.GetComponent<IArrow>().ReturnArrowComponent();
        arrow.GetComponent<Rigidbody>().velocity = -arrow.transform.forward * player.gage.value * ConstInt.basicDamage * 2;
        arrowComponent.onDestroy.AddListener(turn.SubMovingCnt);
        arrowComponent.dmg = ConstInt.basicArrowDamage * player.gage.value;
        arrow.GetComponent<CheckOnGround>().outBorder.AddListener(turn.SubMovingCnt);
        turn.AddMovingCnt();
    }
    public override void ShootArrow(Vector3 arrowDirection, Quaternion transformRotation)
    {
        ShootPolydactyly(transform.forward + polydactylyArrowRight, transform.rotation * Quaternion.Euler(polydactylyArrowRight));
        ShootPolydactyly(transform.forward + polydactylyArrowLeft, transform.rotation * Quaternion.Euler(polydactylyArrowLeft));
        ShootPolydactyly(transform.forward, transform.rotation);
    }
}
