using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stigma : MonoBehaviour
{
    SwordSwingScript swordSwing;
    Player playerScript;
    public UnityEvent addStigmaEvent;

    /*
    private void OnEnable()
    {
        swordSwing = transform.Find("SwordSwing").GetComponent<SwordSwingScript>();
        swordSwing.swingAttacked.AddListener(addStigma);
        addStigmaEvent.AddListener(swordSwing.AddStigma);

        playerScript = transform.GetComponent<Player>();
        playerScript.onExitAttack.AddListener(swordSwing.ResetStigma);
    }

    public void addStigma()
    {
        addStigmaEvent.Invoke();
    }
    */
}
