using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossScripy : MonoBehaviour
{
    Enumy enumyScript;
    public List<GameObject> sheildPivot = new List<GameObject>();
    void Start()
    {
        enumyScript = transform.GetComponent<Enumy>();
        enumyScript.stateEvent.onStartAttack.AddListener(DisableSheild);
        enumyScript.stateEvent.onExitAttack.AddListener(EnableSheild);
    }

    public void DisableSheild()
    {
        for(int i = 0;i < sheildPivot.Count; i++)
        {
            sheildPivot[i].SetActive(false);
        }
    }
    public void EnableSheild()
    {
        for (int i = 0; i < sheildPivot.Count; i++)
        {
            sheildPivot[i].SetActive(true);
        }
    }
}
