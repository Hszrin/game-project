using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMeleeMode
{
    bool CheckMeleeMode();
}
public abstract class Archer : MonoBehaviour, IMeleeMode
{
    public GameObject arrowPrefab;
    public GameObject arrow;
    public TurnManager turn;
    public Transform UI;
    public SetAnimeSpriteType setArrowType;
    public Rigidbody playerRigid { get; protected set; }
    public ChracterComponent chracter { get; protected set; }
    public Player player;
    public GameObject playerObject { get; protected set; }
    public bool meleeMode;

    public void AfterStart()
    {
        setArrowType = transform.Find("Arrow").GetComponent<SetAnimeSpriteType>();
        meleeMode = false;
        playerRigid = transform.GetComponent<Rigidbody>();
        player = transform.GetComponent<Player>();
        chracter = transform.GetComponent<IChracterComponent>().ReturnChracterComponent();
        turn = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        arrowPrefab = Resources.Load<GameObject>("Arrow");
        player.stateEvent.onStartAttack.AddListener(ShootModeSelect);
        UI = transform.Find("UI");
    }
    public void ShootModeSelect()
    {
        if (meleeMode) ShootArcher();
        else ShootArrow(transform.forward, transform.rotation);
    }
    public abstract void ShootArrow(Vector3 arrowDirection, Quaternion transformRotation);
    public void ShootArcher()
    {
        playerRigid.velocity = -transform.forward * player.gage.value * ConstInt.basicDamage * (100 + chracter.speed) / 100;
    }

    public bool CheckMeleeMode()
    {
        if (meleeMode) return true;
        else return false;
    }
}
