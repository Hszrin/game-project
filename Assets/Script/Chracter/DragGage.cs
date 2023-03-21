using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragGage : MonoBehaviour
{
    private float chargingTime = 1f;
    private float chargingSpeed;
    private float minForce = 0.1f;
    private float maxForce = 1f;
    private float maxLength = 4.5f;
    private float currentLength;
    private float fullDistanceMag;
    private Vector3 distanceVec;
    private Slider gage;
    private Vector3 playerPos;
    public GameObject dragPointObject;

    private void Start()
    {
        gage = transform.GetComponent<Slider>();
        chargingSpeed = (maxForce - minForce) / chargingTime;
    }
    public void UpdateLength(Player player)
    {
        playerPos = player.transform.position;
        distanceVec = dragPointObject.transform.position - playerPos;
        fullDistanceMag = Vector3.Magnitude(distanceVec);
        currentLength = maxLength * gage.value;

        if (fullDistanceMag > currentLength + 0.1f && fullDistanceMag > ConstInt.minMovementAccess)
        {
            increaseLength();
        }
        else if(fullDistanceMag < currentLength - 0.1f)
        {
            decreaseLength();
        }
    }
    private void increaseLength()
    {
        if (gage.value + chargingSpeed * Time.deltaTime < 1)
        {
            gage.value += chargingSpeed * Time.deltaTime;
        }
    }
    private void decreaseLength()
    {
        if(gage.value - chargingSpeed * Time.deltaTime > 0)
        {
            gage.value -= chargingSpeed * Time.deltaTime;
        }
    }
}
