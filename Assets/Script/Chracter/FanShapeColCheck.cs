using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanShapeColCheck
{
    public bool SwordColCheck(Transform opponent, Transform myTransform, float leftAngle, float rightAngle)
    {
        Vector3 v = opponent.position - myTransform.position;
        Vector3 myVec = myTransform.forward * -1;
        Vector3 crossVec = Vector3.Cross(v, myVec);

        float theta = Mathf.Acos(Vector3.Dot(v, myVec) / v.magnitude / myVec.magnitude) * Mathf.Rad2Deg;
        float dot = Vector3.Dot(crossVec, Vector3.up);

        if (dot < 0)
        {
            if (Mathf.Abs(theta) < Mathf.Abs(rightAngle)) return true;
            else return false;
        }
        else
        {
            if (Mathf.Abs(theta) < Mathf.Abs(leftAngle)) return true;
            else return false;
        }
    }
    public bool HitColCheck(Transform opponent, Transform myTransform, List<float> leftAngle, List<float> rightAngle)
    {
        Vector3 v = opponent.position - myTransform.position;
        Vector3 myVec = myTransform.forward * -1;
        Vector3 crossVec = Vector3.Cross(v, myVec);

        float theta = Mathf.Acos(Vector3.Dot(v, myVec) / v.magnitude / myVec.magnitude) * Mathf.Rad2Deg;
        float dot = Vector3.Dot(crossVec, Vector3.up);

        if (dot < 0)
        {
            if (rightAngle.Count == 1)
            {
                if (Mathf.Abs(theta) < Mathf.Abs(rightAngle[0])) return true;
                else return false;
            }
            else if (rightAngle.Count == 2)
            {
                if (Mathf.Abs(theta) < Mathf.Abs(rightAngle[0]) || Mathf.Abs(theta) > Mathf.Abs(rightAngle[1])) return true;
                else return false;
            }
            else
            {
                Debug.Log("콜라이더 충돌 각도 에러");
                return false;//임시
            }
        }
        else
        {
            if (leftAngle.Count == 1)
            {
                if (Mathf.Abs(theta) < Mathf.Abs(leftAngle[0])) return true;
                else return false;
            }
            else if (leftAngle.Count == 2)
            {
                if (Mathf.Abs(theta) < Mathf.Abs(leftAngle[0]) || Mathf.Abs(theta) > Mathf.Abs(leftAngle[1])) return true;
                else return false;
            }
            else
            {
                Debug.Log("콜라이더 충돌 각도 에러");
                return false;//임시
            }
        }
    }
}
