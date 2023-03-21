using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frozen : MonoBehaviour
{
    public List<Transform> frozenImages = new List<Transform>();
    public StateEvent myStateEvent;

    private void Start()
    {
        myStateEvent = transform.parent.GetComponent<IStateEvent>().ReturnStateEvent();
        for (int i = 0;i < 8; i++)
        {
            frozenImages.Add(transform.GetChild(i));
        }
    }
    public void EnableFrozen()
    {
        for (int i = 0; i < frozenImages.Count; i++)
        {
            frozenImages[i].gameObject.SetActive(true);
            frozenImages[i].rotation = Quaternion.Euler(new Vector3(90, 0, Random.Range(360 / frozenImages.Count*i, 360 / frozenImages.Count * (i+1))));
        }
        SetFrozen();
    }
    public void SetFrozen()
    {
        myStateEvent.onStartAttack.AddListener(Func);
    }
    public void Func()
    {
        myStateEvent.onExitAttack.AddListener(DisableFrozen);
    }
    public void DisableFrozen()
    {
        myStateEvent.onStartAttack.RemoveListener(Func);
        myStateEvent.onExitAttack.RemoveListener(DisableFrozen);
        for (int i = 0; i < frozenImages.Count; i++)
        {
            frozenImages[i].gameObject.SetActive(false);
        }
    }
}