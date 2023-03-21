using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public interface IArrow 
{
    public ArrowComponent ReturnArrowComponent();
}
public class ArrowComponent
{
    public Vector3 archerPos = new Vector3();
    public float dmg;
    public UnityEvent onDestroy = new UnityEvent();
}
public class ArrowCollision : MonoBehaviour, IArrow
{
    public ArrowComponent arrowComponent = new ArrowComponent();
    private ChracterComponent opponentComponent;
    private void Start()
    {
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

            collision.rigidbody.velocity = Vector3.zero;
            arrowComponent.onDestroy.Invoke();
        }
        if (collision.transform.tag == "Obstacle")
        {
            if (collision.gameObject.TryGetComponent(out ISheildObject sheildObject))
            {
                sheildObject.AdjustSheildHp(-1);
            }
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
