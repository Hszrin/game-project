using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAndBoard : MonoBehaviour
{
    GameObject sheild;
    Player player;
    private void OnEnable()
    {
        player = transform.GetComponent<Player>();
        player.resetSkill.AddListener(ActiveSheild);
        sheild = transform.Find("SheildPivot").gameObject;

        player.chracterComponent.leftAttackAngle.Clear();
        player.chracterComponent.leftAttackAngle.Add(25);
        player.chracterComponent.leftAttackAngle.Add(155);
        ActiveSheild();
    }

    public void ActiveSheild()
    {
        sheild.SetActive(true);
    }
}
