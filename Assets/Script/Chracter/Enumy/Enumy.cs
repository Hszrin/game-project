using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
public interface IEState
{
    void OnEnter(Enumy enumy);
    void Update();
    void OnExit();
}
public class Enumy : MonoBehaviour, IAttacked, IChracterComponent, IStateEvent
{ 
    public Slider hpSlider;
    public Rigidbody enumyRigid;
    public HitProcess hitProcess;
    public ChracterComponent chracterComponent;
    public TurnManager turn;
    private GameObject hpPlusTextPrefab;
    private GameObject hpPlusTextCopy;
    private GameObject damageTextPrefab;
    private GameObject damageTextCopy;
    public StateEvent stateEvent = new StateEvent();

    public string state;

    private IEState currentState;

    private void Start()
    {
        hitProcess = transform.GetComponent<HitProcess>();
        turn = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        hpPlusTextPrefab = Resources.Load("HpPlusText") as GameObject;
        damageTextPrefab = Resources.Load("DamageText") as GameObject;
        SetState(new IdleEState());

        chracterComponent.chracter = gameObject;
        chracterComponent.IChracter = transform.GetComponent<IChracterComponent>();
        chracterComponent.HPUI = transform.Find("HPUI");
        chracterComponent.hpSlider = chracterComponent.HPUI.Find("HP").GetComponent<Slider>();
        chracterComponent.stigma = false;
        chracterComponent.OnDestroyEvent.AddListener(OnDestroyNotify);
        state = "Idle";
    }
    public void OnDestroyNotify()
    {
        turn.SubMovingCnt();
        EnumyInfo.Instance.RemoveEnumy(gameObject);
    }
    private void Update()
    {
        currentState.Update();
    }
    public void HpPlusTextFloating(float val)
    {
        hpPlusTextCopy = hpPlusTextPrefab;
        hpPlusTextCopy.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = val.ToString();
        hpPlusTextCopy = Instantiate(hpPlusTextPrefab, transform.Find("HPUI"));
    }
    public void HpMinusTextFloating(float val)
    {
        damageTextCopy = damageTextPrefab;
        damageTextCopy.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = val.ToString();
        damageTextCopy = Instantiate(damageTextCopy);
        damageTextCopy.transform.localPosition = transform.position;
    }
    public void SetState(IEState nextState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = nextState;
        currentState.OnEnter(this);
    }
    public void setStateAttack()
    {
        SetState(new AttackEState());
        if (hpSlider.value > 0) EnterAttackEvent();
    }
    public HitProcess getHitProcess()
    {
        return hitProcess;
    }
    public ChracterComponent ReturnChracterComponent()
    {
        return chracterComponent;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.transform.tag == "Player" || collision.transform.tag == "Enumy")&&state == "Attack")
        {
            Debug.Log("enumy");
            hitProcess.CollisionProcess(collision, new CollisionComponent
            {
                leftAttackAngle = chracterComponent.leftAttackAngle,
                rightAttackAngle = chracterComponent.rightAttackAngle,
                dmg = chracterComponent.power = 10,
                speed = chracterComponent.speed
            });
        }
        else if (collision.transform.tag == "Obstacle" && state == "Attack")
        {
            hitProcess.CollisionObstacle(collision);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "Enumy")
        {
            Vector3 direction = collision.transform.position - transform.position;
            collision.transform.position += direction/100;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if ((collision.transform.tag == "Player" || collision.transform.tag == "Enumy") && state == "Attack")
        {
            collision.transform.GetComponent<IAttacked>().setStateAttack();
        }
    }
    public void StartAttackEvent() { stateEvent.onStartAttack.Invoke(); }
    public void EnterAttackEvent() { stateEvent.onEnterAttack.Invoke(); }
    public void ExitAttackEvent() { stateEvent.onExitAttack.Invoke(); }
    public void Shoot()
    {
        List<Vector3> playerPoses = new List<Vector3>();

        for(int i = 0;i < PlayerInfo.Instance.enabledPlayers.Count;i++)
        {
            playerPoses.Add(PlayerInfo.Instance.enabledPlayers[i].transform.position - transform.position);
            if (PlayerInfo.Instance.enabledPlayers[i] != null) playerPoses.Add(PlayerInfo.Instance.enabledPlayers[i].transform.position - transform.position);
        }
        if (playerPoses.Count > 0)
        {
            Vector3 shortest = playerPoses[0];
            for (int i = 0; i < playerPoses.Count; i++)
            {
                if (shortest.magnitude > playerPoses[i].magnitude) shortest = playerPoses[i];
            }
            SetState(new AttackEState());
            enumyRigid.velocity = shortest.normalized * (chracterComponent.speed + 100)/100*ConstInt.basicShootCoff;
            turn.StartCounting();
        }
    }
    public StateEvent ReturnStateEvent()
    {
        return stateEvent;
    }
}
public class IdleEState : IEState
{
    private Enumy enumy;
    void IEState.OnEnter(Enumy enumy)
    {
        this.enumy = enumy;
        enumy.state = "idle";
    }
    void IEState.Update()
    {

    }
    void IEState.OnExit()
    {

    }
}
public class AttackEState : IEState
{
    private Enumy enumy;

    void IEState.OnEnter(Enumy enumy)
    {
        this.enumy = enumy;
        enumy.state = "Attack";
    }
    void IEState.Update()
    {
        if (enumy.enumyRigid.velocity.magnitude < 0.05f)
        {
            enumy.SetState(new IdleEState());
        }
    }
    void IEState.OnExit()
    {
        enumy.ExitAttackEvent();
    }
}
