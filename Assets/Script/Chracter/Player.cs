using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public interface IState {
    void OnEnter(Player player);
    void Update();
    void OnExit();
}
public interface IAttacked
{
    void setStateAttack();
    HitProcess getHitProcess();
}
public interface IStateEvent
{
    StateEvent ReturnStateEvent();
}
public class StateEvent
{
    public UnityEvent onStartAttack = new UnityEvent();
    public UnityEvent onEnterAttack = new UnityEvent();
    public UnityEvent onExitAttack = new UnityEvent();
    public UnityEvent onEnterCharging = new UnityEvent();
    public UnityEvent onChargingToIdle = new UnityEvent();
}
public class Player : MonoBehaviour, IAttacked, IChracterComponent, IStateEvent, IPlayerSpecialStatList
{
    public Transform dragPoint;
    public Slider gage;
    public Rigidbody playerRigid;
    public DragPoint dragScript;
    public HitProcess hitProcess;
    public Rotation rotation;
    public Vector3 forCheckDistanceFixPos = new Vector3();
    private GameObject hpPlusTextPrefab;
    private GameObject hpPlusTextCopy;
    private GameObject damageTextPrefab;
    private GameObject damageTextCopy;
    private SwordSwingScript swordSwingScript;
    public StateEvent stateEvent = new StateEvent();
    public TurnManager turn { get; protected set; }

    public PlayerSpecialStatList playerSpecialStats = new PlayerSpecialStatList();
    public CollisionComponent collisionComponent = new CollisionComponent();

    public ChracterComponent chracterComponent;
    
    public bool attackUsed;

    public int slot;
    public string state;
    public IState currentState;

    public UnityEvent resetSkill;
    public UnityEvent onCollisionEnterEvent;

    private void Awake()
    {
        rotation = new Rotation();
        hitProcess = transform.GetComponent<HitProcess>();
        turn = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        SetState(new IdleState());
        hpPlusTextPrefab = Resources.Load("HpPlusText") as GameObject;
        damageTextPrefab = Resources.Load("DamageText") as GameObject;
        state = "Idle";
        chracterComponent.chracter = gameObject;
        chracterComponent.IChracter = transform.GetComponent<IChracterComponent>();
        chracterComponent.HPUI = transform.Find("HPUI");
        chracterComponent.hpSlider = chracterComponent.HPUI.Find("HP").GetComponent<Slider>(); 
        chracterComponent.stigma = false;
        chracterComponent.OnDestroyEvent.AddListener(DestroyMyself);
        chracterComponent.OnDestroyEvent.AddListener(turn.CheckPlayerCount);

        stateEvent.onStartAttack.AddListener(turn.StartCounting);
        stateEvent.onEnterAttack.AddListener(turn.AddMovingCnt);
        stateEvent.onExitAttack.AddListener(turn.SubMovingCnt);
    }
    private void Update()
    {
        currentState.Update();
    }

    public void DestroyMyself()
    {
        turn.SubMovingCnt();
        gameObject.SetActive(false);
        PlayerInfo.Instance.RemovePlayer(gameObject);
    }

    public void HpPlusTextFloating(float val)
    {
        hpPlusTextCopy = hpPlusTextPrefab;
        hpPlusTextCopy.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = val.ToString();
        hpPlusTextCopy = Instantiate(hpPlusTextCopy);
        hpPlusTextCopy.transform.localPosition = transform.position;
    }
    public void HpMinusTextFloating(float val)
    {
        damageTextCopy = damageTextPrefab;
        damageTextCopy.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = val.ToString();
        damageTextCopy = Instantiate(damageTextCopy);
        damageTextCopy.transform.localPosition = transform.position;
    }
    public void ResetSkill()
    {
        resetSkill.Invoke();
        if (gameObject.activeSelf == false)
        { 
            gameObject.SetActive(true);
            PlayerInfo.Instance.AddPlayer(gameObject);
        }
        chracterComponent.HpAdjust(20);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.transform.tag == "Player" || collision.transform.tag == "Enumy") && state == "Attack")
        {
            hitProcess.CollisionProcess(collision, new CollisionComponent
            {
                leftAttackAngle = chracterComponent.leftAttackAngle,
                rightAttackAngle = chracterComponent.rightAttackAngle,
                dmg = chracterComponent.power = 10,
                speed = chracterComponent.speed
            });
            onCollisionEnterEvent.Invoke();
        }
        else if(collision.transform.tag == "Obstacle" && state == "Attack")
        {
            hitProcess.CollisionObstacle(collision);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "Enumy")
        {
            Vector3 direction = collision.transform.position - transform.position;
            collision.transform.position += direction / 100;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if ((collision.transform.tag == "Player" || collision.transform.tag == "Enumy") && state == "Attack")
        {
            collision.transform.GetComponent<IAttacked>().setStateAttack();
        }
    }
    public void SetState(IState nextState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }

        currentState = nextState;
        currentState.OnEnter(this);
    }
    public void SetChargingToIdleState()
    {
        SetState(new IdleState());
        stateEvent.onChargingToIdle.Invoke();
    }
    public void SetWaitingToIdleState()
    {
        SetState(new IdleState());
        attackUsed = false;
    }
    public void SetChargingState()
    {
        if (state == "Idle") SetState(new ChargingState());
    }
    public void ChargingToAttackState()
    {
        SetState(new AttackState());
        attackUsed = true;
        StartAttackEvent();
    }
    public void IdleToWaitState()
    {
        SetState(new WaitingState());
    }
    public void setStateAttack()
    {
        SetState(new AttackState());
        if (chracterComponent.hpSlider.value > 0) EnterAttackEvent();
    }
    public void StartAttackEvent() { stateEvent.onStartAttack.Invoke(); }
    public void EnterAttackEvent() { stateEvent.onEnterAttack.Invoke(); }
    public void ExitAttackEvent() { stateEvent.onExitAttack.Invoke(); }
    public HitProcess getHitProcess()
    {
        return hitProcess;
    }
    public ChracterComponent ReturnChracterComponent()
    {
        return chracterComponent;
    }
    public StateEvent ReturnStateEvent()
    {
        return stateEvent;
    }
    public PlayerSpecialStatList ReturnPlayerSpecialStatList()
    {
        return playerSpecialStats;
    }
}

public class IdleState : IState
{
    private Player player;

    void IState.OnEnter(Player player)
    {
        this.player = player;
        player.state = "Idle";
        player.forCheckDistanceFixPos = player.transform.position;
        player.dragPoint.gameObject.SetActive(true);
    }
    void IState.Update()
    {
    }
    void IState.OnExit()
    {
    }
}

public class ChargingState : IState
{
    private Player player;
    private DragGage chargingScript;

    void IState.OnEnter(Player player)
    {
        this.player = player;
        chargingScript = player.gage.GetComponent<DragGage>();
        player.state = "Charging";
        player.stateEvent.onEnterCharging.Invoke();
    }
    void IState.Update()
    {
        chargingScript.UpdateLength(player);
        player.rotation.RotateChracter(player.dragPoint, player);
    }
    void IState.OnExit()
    {

    }
}

public class AttackState : IState
{
    private Player player;
    void IState.OnEnter(Player player)
    {
        this.player = player;
        player.state = "Attack";
        player.dragPoint.gameObject.SetActive(false);
    }
    void IState.Update()
    {
        if (player.playerRigid.velocity.magnitude < 0.05f)
        {
            player.SetState(new WaitingState());
        }
    }
    void IState.OnExit() 
    {
        player.ExitAttackEvent();
        player.dragScript.ResetDragPoint();
    }
}
public class WaitingState : IState
{
    private Player player;
    void IState.OnEnter(Player player)
    {
        this.player = player;
        player.state = "Waiting";
        player.dragScript.ResetDragPoint();
    }
    void IState.Update()
    {

    }
    void IState.OnExit()
    {

    }
}