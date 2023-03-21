using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowAnimScript : MonoBehaviour
{
    public Animator anime;
    public Slider gage;
    public Transform archer;
    public List<GameObject> magicHeadList = new List<GameObject>();
    public GameObject magicHead;
    void Start()
    {
        archer = transform.parent;
        archer.GetComponent<Player>().stateEvent.onStartAttack.AddListener(StartExit);
        archer.GetComponent<Player>().stateEvent.onExitAttack.AddListener(StartExit);
        archer.GetComponent<Player>().stateEvent.onChargingToIdle.AddListener(StartExit);
    }
    public void StartExit()
    {
        if (!archer.GetComponent<IMeleeMode>().CheckMeleeMode())
        {
            for(int i = 0;i < magicHeadList.Count; i++)
            {
                magicHeadList[i].SetActive(false);
            }
            anime.SetBool("startExit", true);
            anime.SetBool("gage", false);
        }
    }
    public void StartShoot()
    {
        if (!archer.GetComponent<IMeleeMode>().CheckMeleeMode() && anime.GetBool("gage") == false && gage.value > 0.33f)
        {
            magicHead.SetActive(true);
            anime.SetBool("startExit", false);
            anime.SetBool("gage", true);
        }
    }
    private void Update()
    {
        StartShoot();
    }
}
