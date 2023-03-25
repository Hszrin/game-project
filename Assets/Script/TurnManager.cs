using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
public class TurnManager : MonoBehaviour
{
    public int turn;
    public GameObject currentEnumy;
    public GameObject cloneEnumy;
    public GameObject enumyPrefab;
    public GameObject enumyClone;
    public Counting cnt;
    public UnityEvent gameEnd;
    public UnityEvent exitCounting;
    private bool playerTurn = true;
    private void Start()
    {
        turn = 0;
    }
    public void InstantiateEnumy(string prefabName, Vector3 instantiatePos)
    {
        enumyPrefab = Resources.Load<GameObject>(prefabName);
        enumyClone = Instantiate(enumyPrefab, instantiatePos, Quaternion.identity);
        enumyClone.GetComponent<Enumy>().stateEvent.onStartAttack.AddListener(StartCounting);
        enumyClone.GetComponent<Enumy>().stateEvent.onEnterAttack.AddListener(AddMovingCnt);
        enumyClone.GetComponent<Enumy>().stateEvent.onExitAttack.AddListener(SubMovingCnt);
        EnumyInfo.Instance.GetAllEnumyList();
    }
    public void StartCounting()
    {
        Debug.Log("startCounting");
        cnt = new Counting(this);
    }
    public void ExitCounting()
    {
        Debug.Log("ExitCounting");
        turn++;
        Debug.Log(turn);
        if (turn >= PlayerInfo.Instance.enabledPlayers.Count && playerTurn)
        {
            //Debug.Log("playerturn End");
            playerTurnEnd();
            PlayerInfo.Instance.UpdatePlayerList();
        }
        if (turn >= EnumyInfo.Instance.currentTurn.maxCount && !playerTurn)
        {
            //Debug.Log("Enumy turn End");
            enumyTurnEnd();
            PlayerInfo.Instance.UpdatePlayerList();
        }
        if (playerTurn)
        {
            //Debug.Log("player turn cunduct");
            cnt = null;
            playerTurnConduct();
        }
        else
        {
            //Debug.Log("enumy turn cunduct");
            cnt = null;
            enumyTurnConduct();
        }
        exitCounting.Invoke();
    }
    public void playerTurnConduct()//공격 안한 캐릭터들 잠금 해제
    {
        for (int i = 0; i < PlayerInfo.Instance.enabledPlayers.Count; i++)
        {
            if (PlayerInfo.Instance.enabledPlayers[i] != null)
            {
                if (!PlayerInfo.Instance.enabledPlayers[i].GetComponent<Player>().attackUsed)
                    PlayerInfo.Instance.enabledPlayers[i].GetComponent<Player>().SetWaitingToIdleState();
            }
        }
    }
    public void playerTurnEnd()
    {
        playerTurn = false;
        turn = 0;
    }
    public void enumyTurnConduct()
    {
        Enumy enumyScript;
        currentEnumy = EnumyInfo.Instance.enumys[EnumyInfo.Instance.currentTurn.index];
        EnumyInfo.Instance.currentTurn.addIndex();
        enumyScript = currentEnumy.GetComponent<Enumy>();
        enumyScript.Shoot();
    }
    public void enumyTurnEnd()
    {
        for (int i = 0; i < PlayerInfo.Instance.enabledPlayers.Count; i++)
        {
            if (PlayerInfo.Instance.enabledPlayers[i] != null) PlayerInfo.Instance.enabledPlayers[i].GetComponent<Player>().SetWaitingToIdleState();
        }
        turn = 0;
        playerTurn = true;
    }
    public void WaveEnd()
    {
        turn = 0;
        for (int i = 0; i < PlayerInfo.Instance.enabledPlayers.Count; i++)
        {
            if (PlayerInfo.Instance.enabledPlayers[i] != null) PlayerInfo.Instance.enabledPlayers[i].GetComponent<Player>().SetWaitingToIdleState();
        }
        cnt = null;
    }
    private void Update()
    {
        if (cnt != null)
        {
            cnt.Update();

        }
    }
    public void AddMovingCnt()
    {
        Debug.Log("add");
        if(cnt != null) cnt.movingObject++;
    }
    public void SubMovingCnt()
    {
        Debug.Log("sub");
        if (cnt != null) cnt.movingObject--;
    }
    public void CheckPlayerCount()
    {
        if (PlayerInfo.Instance.enabledPlayers.Count == 0) gameEnd.Invoke();
    }
}
public class Counting
{
    public int movingObject;
    private TurnManager turn;
    public Counting(TurnManager turn)
    {
        this.turn = turn;
        movingObject = 1;
    }
    public void Update()
    {
        if (movingObject <= 0)
        {
            turn.ExitCounting();
            EnumyInfo.Instance.CheckEnumyCount();
        }
    }
}