using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation
{

    private Vector3 rotation;
    public void RotateChracter(Transform target, Player player)
    {
        rotation = target.position - player.transform.position;
        if (Vector3.Magnitude(rotation) > ConstInt.minMovementAccess)
        {
            player.transform.rotation = Quaternion.LookRotation(rotation);
        }
    }
}
