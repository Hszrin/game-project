using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ISwordComponent
{
    public SwordComponent ReturnSwordComponent();
}
public class Knight : MonoBehaviour, ISwordComponent
{
    public GameObject rightSword;
    public GameObject leftSword;
    private Rigidbody playerRigid;
    private Player player;
    private ChracterComponent chracter;
    public TurnManager turn;
    public Vector3 afterRotation = new Vector3(90, 0, 0);
    public UnityEvent swing;
    public UnityEvent swingReset;
    public UnityEvent onActivateRightSword;
    public UnityEvent onActivateLeftSword;
    public UnityEvent onDeActivateRightSword;
    public UnityEvent onDeActivateLeftSword;
    public SwordComponent swordComponent = new SwordComponent();
    private void Start()
    {
        playerRigid = transform.GetComponent<Rigidbody>();
        transform.GetComponent<Player>().playerSpecialStats.AddSpecialList("검 공격력", swordComponent.swordDmg);
        player = transform.GetComponent<Player>();
        chracter = transform.GetComponent<IChracterComponent>().ReturnChracterComponent();
        player.stateEvent.onStartAttack.AddListener(ShootKnight);
        onActivateRightSword.AddListener(ActiveRightSword);
        onDeActivateRightSword.AddListener(DeActiveRightSword);
    }
    private void Update()
    {
        if (player.gage.value >= 0.8)
        {
            onActivateRightSword.Invoke();
            onActivateLeftSword.Invoke();
        }
        else if(player.gage.value < 0.8 && player.state == "Charging")
        {
            onDeActivateRightSword.Invoke();
            onDeActivateLeftSword.Invoke();
        }
    }
    public void ShootKnight()
    {
        playerRigid.velocity = -transform.forward * player.gage.value * ConstInt.basicShootCoff * (100 + chracter.speed) / 100;
        if (player.gage.value >= 0.8)
        {
            StartCoroutine(SwingDelay());
        }
    }
    IEnumerator SwingDelay()
    {
        yield return new WaitForSeconds(player.gage.value);
        swing.Invoke();
        turn.AddMovingCnt();
        yield return new WaitForSeconds(0.1f);
        swingReset.Invoke();
        turn.SubMovingCnt();
    }
    public void ActiveRightSword()
    {
        rightSword.SetActive(true);
    }
    public void DeActiveRightSword()
    {
        rightSword.SetActive(false);
    }
    public void ActiveLeftSword()
    {
        leftSword.SetActive(true);
    }
    public void DeActiveLeftSword()
    {
        leftSword.SetActive(false);
    }
    public SwordComponent ReturnSwordComponent()
    {
        return swordComponent;
    }
}
