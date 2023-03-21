using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DragPoint : MonoBehaviour
{
    public bool isCharging = false;
    public bool isFired = false;
    public Transform playerPos;
    public Slider gage;
    private void OnMouseDown()
    {
        
        Vector3 objectPos = GetMouseWorldPosition();
        objectPos.y = playerPos.position.y;
        transform.position = objectPos;
        TurnChargingState();
    }
    void OnMouseDrag()
    {
        Vector3 objectPos = GetMouseWorldPosition();
        objectPos.y = playerPos.position.y;
        transform.position = objectPos;
    }
    private void OnMouseUp()
    {
        if (gage.value * ConstInt.dragGageCoff < ConstInt.minMovementAccess)
        {
            TurnIdleState();
        }
        else
        {
            TurnAttackState();
        }
        ResetDragPoint();
        gage.value = 0;
    }
    Vector3 GetMouseWorldPosition()
    {
        var mousePos = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(mousePos + new Vector3(0, 0, 10f));
    }

    public UnityEvent setIdle;
    public UnityEvent setCharging;
    public UnityEvent setAttack;
    public void TurnIdleState() { setIdle.Invoke(); }
    public void TurnChargingState() { setCharging.Invoke(); }
    public void TurnAttackState() { setAttack.Invoke(); }

    public void ResetDragPoint()
    {
        transform.position = playerPos.position;
    }
}
