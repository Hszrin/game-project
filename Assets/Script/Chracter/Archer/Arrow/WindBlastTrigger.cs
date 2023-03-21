using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBlastTrigger : MonoBehaviour
{
    public float blastDmg;
    private void Start()
    {
        blastDmg = 15;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" || other.transform.tag == "Enumy")
        {
            Vector3 direction = other.transform.position - transform.position;
            float distance = direction.magnitude;
            other.transform.GetComponent<Rigidbody>().velocity = direction.normalized * blastDmg * BlastConstFunc((2.75f - distance) / 2.75f);
            other.transform.GetComponent<IAttacked>().setStateAttack();
        }
    }
    float BlastConstFunc(float x)
    {
        return Mathf.Pow(x, 8) + 1;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
