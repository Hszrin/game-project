using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    public Animator anime;
    public Slider gage;
    public Transform archer;
    public IMeleeMode meleeMode;
    public bool startExit;
    public bool startCharging;
    public UnityEvent startExitEvent;
    public UnityEvent startShootEvent;
    private void Start()
    {
        startExit = false;
        startCharging = false;
        archer = transform.parent;
        archer.GetComponent<Player>().stateEvent.onEnterCharging.AddListener(StartCharging);
        archer.GetComponent<Player>().stateEvent.onStartAttack.AddListener(StartExit);
        archer.GetComponent<Player>().stateEvent.onExitAttack.AddListener(StartExit);
        archer.GetComponent<Player>().stateEvent.onChargingToIdle.AddListener(StartExit);
    }
    public void StartExit()
    {
        if (!archer.GetComponent<IMeleeMode>().CheckMeleeMode())
        {
            anime.SetBool("startExit", true);
            anime.SetBool("startCharging", false);
            anime.SetBool("gage", false);
        }
    }
    public void StartCharging()
    {
        if (!archer.GetComponent<IMeleeMode>().CheckMeleeMode())
        {
            anime.SetBool("startExit", false);
            anime.SetBool("startCharging", true);
        }
    }
    public void StartShoot()
    {
        if(!archer.GetComponent<IMeleeMode>().CheckMeleeMode() && anime.GetBool("gage") == false  && gage.value>0.33f)
        {
            anime.SetBool("gage", true);
        }
    }
    private void Update()
    {
        StartShoot();
    }
}
