using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperArrowCollision : MonoBehaviour, IArrow
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
            if (CheckSniped(collision))
            {
                opponentComponent.HpAdjust(-Mathf.Ceil(arrowComponent.dmg * 2));
            }
            else opponentComponent.HpAdjust(-Mathf.Ceil(arrowComponent.dmg));

            collision.rigidbody.velocity = Vector3.zero;
            arrowComponent.onDestroy.Invoke();
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
    public bool CheckSniped(Collision other)
    {
        
        Vector3 contactToOppnent = other.transform.position - other.contacts[0].point;
        contactToOppnent = new Vector3(contactToOppnent.x, 0, contactToOppnent.z);
        Vector3 archerToOppnent = other.transform.position - arrowComponent.archerPos;
        float dotVec = Vector3.Dot(contactToOppnent, archerToOppnent);

        float theta = Mathf.Acos(dotVec/ contactToOppnent.magnitude/ archerToOppnent.magnitude) * Mathf.Rad2Deg;
        if(theta < 10) return true;
        return false;
    }
}
