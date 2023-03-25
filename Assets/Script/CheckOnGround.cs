using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckOnGround : MonoBehaviour
{
    private ChracterComponent myHp;

    public GameObject ground;
    BoxCollider groundCol;
    private TurnManager turn;
    float x;
    float z;
    float xPlus;
    float zPlus;

    public UnityEvent outBorder;

    private void Start()
    {
        ground = GameObject.FindGameObjectWithTag("Ground");
        turn = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        groundCol = ground.GetComponent<BoxCollider>();
        x = -groundCol.bounds.size.x/2 + ground.transform.position.x;
        z = -groundCol.bounds.size.z / 2 + ground.transform.position.z;
        xPlus = groundCol.bounds.size.x / 2 + ground.transform.position.x;
        zPlus = groundCol.bounds.size.z / 2 + ground.transform.position.z;
    }
    private void Update()
    {
        if(transform.position.x > xPlus || transform.position.x < x || transform.position.z > zPlus || transform.position.z < z)
        {
            if (transform.TryGetComponent<IChracterComponent>(out IChracterComponent myIHp))
            {
                myHp = myIHp.ReturnChracterComponent();
                myHp.HpAdjust(-999999);
                turn.SubMovingCnt();
            }
            else
            {
                Destroy(gameObject); //캐릭터 제외 오브젝트 파괴
            }
            outBorder.Invoke();
        }
    }
}
