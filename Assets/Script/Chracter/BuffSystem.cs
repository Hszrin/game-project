using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuffSystem : MonoBehaviour
{
    public UnityEvent buffMyTurnSubEvent;
    public UnityEvent buffGameTurnSub;
    public SpeedBuff speedBuff;
    public StateEvent myStateEvent;
    public ChracterComponent myComponent;
    private void Start()
    {
        myComponent = transform.GetComponent<IChracterComponent>().ReturnChracterComponent();
        myStateEvent = transform.GetComponent<IStateEvent>().ReturnStateEvent();
    }

    public void SetSpeedBuff(int turn, float speed)
    {
        speedBuff = new SpeedBuff(turn, speed);
        speedBuff.AddBuff(myComponent);
        buffMyTurnSubEvent.AddListener(speedBuff.SubTurn);
        buffMyTurnSubEvent.AddListener(CheckSpeedBuff);
        myStateEvent.onStartAttack.AddListener(BuffMyTurnSub);
    }
    public void CheckSpeedBuff()
    {
        if (speedBuff.CheckBuff())
        {
            speedBuff.RemoveBuff(myComponent);
            buffMyTurnSubEvent.RemoveListener(speedBuff.SubTurn);
            buffMyTurnSubEvent.RemoveListener(CheckSpeedBuff);
            myStateEvent.onStartAttack.RemoveListener(BuffMyTurnSub);
            speedBuff = null;
        }
    }
    public void BuffMyTurnSub()
    {
        buffMyTurnSubEvent.Invoke();
    }
}
public class SpeedBuff
{
    public float speed;
    public int turn;
    
    public SpeedBuff(int turn, float speed)
    {
        this.speed = speed;
        this.turn = turn;
    }
    public void SubTurn()
    {
        turn--;
    }
    public void AddBuff(ChracterComponent component)
    {
        component.speed += speed;
    }
    public void RemoveBuff(ChracterComponent component)
    {
        component.speed -= speed;
    }
    public bool CheckBuff()
    {
        if (turn == 0) return true;
        return false;
    }
}
