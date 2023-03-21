using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSheildRotation : MonoBehaviour
{
    public Vector3 degree = new Vector3(0, 1, 0);
    public Quaternion transformRotation;
    private void Start()
    {
        transformRotation = transform.rotation;
    }
    void Update()
    {
        degree.y += Time.deltaTime * 30;
        transform.rotation = Quaternion.Euler(degree) * transformRotation;
    }
}
