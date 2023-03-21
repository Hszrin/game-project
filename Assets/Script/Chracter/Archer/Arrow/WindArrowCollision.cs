using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArrowCollision : MonoBehaviour, IArrow
{
    public ArrowComponent arrowComponent = new ArrowComponent();
    private ChracterComponent opponentComponent;
    private GameObject blastPrefab;
    private GameObject blastPrefabCopy;
    private GameObject blastTrigger;
    public Transform blastTriggerPos;
    private void Start()
    {
        blastPrefab = Resources.Load<GameObject>("WindBlastCol");
        blastPrefabCopy = blastPrefab;
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
            blastTrigger = Instantiate(blastPrefabCopy, collision.contacts[0].point, Quaternion.Euler(new Vector3(90, 0, 0)));
            blastTrigger.SetActive(true);
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
}
