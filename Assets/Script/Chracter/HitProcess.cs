using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitProcess : MonoBehaviour
{
    public Vector3 myVelocity;
    private Vector3 normalVec;
    private ChracterComponent opponentComponent;
    private CollisionComponent myDmgComponent;
    private Rigidbody myRigidBody;
    private Transform opponent;
    private FanShapeColCheck fanShapeColCheck = new FanShapeColCheck();

    private void Awake()
    {
        myRigidBody = transform.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        myVelocity = myRigidBody.velocity;
    }
    public void CollisionProcess(Collision collision, CollisionComponent dmgComponent, Transform specTransform = null)
    {
        Debug.Log(fanShapeColCheck.HitColCheck(collision.transform, transform, dmgComponent.leftAttackAngle, dmgComponent.rightAttackAngle));
        if (fanShapeColCheck.HitColCheck(collision.transform, transform, dmgComponent.leftAttackAngle, dmgComponent.rightAttackAngle))
        {
            opponentComponent = collision.transform.GetComponent<IChracterComponent>().ReturnChracterComponent();
            myDmgComponent = dmgComponent;

            if (specTransform != null) normalVec = collision.transform.position - specTransform.position;
            else normalVec = collision.transform.position - transform.position;
            Vector3 dir = Vector3.Reflect(myVelocity.normalized, normalVec).normalized * myVelocity.magnitude;
            transform.GetComponent<Rigidbody>().velocity = dir / 2;
            Vector3 colDir = (-dir.normalized + myVelocity.normalized).normalized;
            collision.transform.GetComponent<Rigidbody>().velocity = (100 - opponentComponent.weight) / 100 * colDir * myVelocity.magnitude / 2;

            hpAdjust(myVelocity.magnitude);
        }
    }
    public void CollisionObstacle(Collision collision)
    {
        Debug.Log("obs");
        Vector3 dir = Vector3.Reflect(myVelocity.normalized, collision.contacts[0].normal)*myVelocity.magnitude;
        transform.GetComponent<Rigidbody>().velocity = dir;

        if(collision.gameObject.TryGetComponent(out ISheildObject sheildObject))
        {
            opponent = sheildObject.ReturnSheildObject();
            dir = Vector3.Reflect(myVelocity.normalized, collision.transform.position - transform.position).normalized * myVelocity.magnitude;
            Vector3 colDir = (-dir.normalized + myVelocity.normalized).normalized * myVelocity.magnitude;
            opponent.GetComponent<Rigidbody>().velocity = (100 - opponent.GetComponent<IChracterComponent>().ReturnChracterComponent().weight) / 100 * colDir / 2;
            sheildObject.AdjustSheildHp(-1);
        }
    }
    private void hpAdjust(float playerVelocity)
    {
        opponentComponent.HpAdjust(-Mathf.Ceil(myDmgComponent.dmg * playerVelocity / ((myDmgComponent.speed + 100) / 100 * ConstInt.basicDamage)));
    }
}
public class CollisionComponent
{
    public List<float> leftAttackAngle;
    public List<float> rightAttackAngle;
    public float dmg;
    public float speed;
}