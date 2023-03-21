using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSpecialStat : MonoBehaviour
{
    Player myPlayerScript;
    public void OnEnable()
    {
        myPlayerScript = transform.GetComponent<Player>();

    }

}