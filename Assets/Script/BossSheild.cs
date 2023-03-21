using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossSheild : MonoBehaviour
{
    Transform bossTransform;
    Vector3 colVelocity;
    public Transform sheildIn;
    public Transform sheildOut;
    public BoxCollider sheildInCol;
    public BoxCollider sheildOutCol;

    private void Start()
    {
        bossTransform = transform.parent.parent;
        sheildInCol = sheildIn.GetComponent<BoxCollider>();
        sheildOutCol = sheildOut.GetComponent<BoxCollider>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "Enumy")
        {
            Vector3 collisionPos = collision.transform.position;
            Vector3 distance = collisionPos - bossTransform.position;
            sheildInCol.isTrigger = true;
            sheildOutCol.isTrigger = true;
            sheildOut.tag = "Untagged";
            colVelocity = collision.transform.GetComponent<IAttacked>().getHitProcess().myVelocity;
            collision.transform.GetComponent<Rigidbody>().velocity = colVelocity;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        sheildInCol.isTrigger = false;
        sheildOutCol.isTrigger = false;
        sheildOut.tag = "Obstacle";
    }
}
