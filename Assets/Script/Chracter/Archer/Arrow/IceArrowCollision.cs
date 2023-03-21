using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IceArrowCollision : MonoBehaviour, IArrow
{
    public ArrowComponent arrowComponent = new ArrowComponent();
    private ChracterComponent opponentComponent;
    public int buffLength;
    public float speedDebuff;
    private void Start()
    {
        buffLength = 1;
        speedDebuff = -30;
        arrowComponent.onDestroy.AddListener(OnDestroyArrow);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Archer") transform.GetComponent<BoxCollider>().isTrigger = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.transform.tag == "Player" || collision.transform.tag == "Enumy") && collision.gameObject.name != "Archer")
        {
            opponentComponent = collision.transform.GetComponent<IChracterComponent>().ReturnChracterComponent();
            opponentComponent.HpAdjust(-Mathf.Ceil(arrowComponent.dmg));
            collision.transform.GetComponent<BuffSystem>().SetSpeedBuff(buffLength, speedDebuff);
            arrowComponent.onDestroy.Invoke();
            collision.transform.Find("FrozenImage").GetComponent<Frozen>().EnableFrozen();
            collision.rigidbody.velocity = Vector3.zero;
        }
        if (collision.transform.tag == "Obstacle")
        {
            arrowComponent.onDestroy.Invoke();
        }
    }

    public void OnDestroyArrow()
    {
        Destroy(gameObject);
    }
    public ArrowComponent ReturnArrowComponent()
    {
        return arrowComponent;
    }
}
