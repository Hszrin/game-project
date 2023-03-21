using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcherAttackMode : MonoBehaviour
{
    GameObject archer;
    Archer archerScript;
    public Color toggleColor;
    public bool toggle;
    private Image buttonImage;
    private Color originalColor;
    private void Start()
    {
        toggle = false;
        buttonImage = transform.GetComponent<Image>();
        originalColor = transform.GetComponent<Image>().color;
        for (int i = 0;i < PlayerInfo.Instance.players.Count; i++)
        {
            if(PlayerInfo.Instance.players[i].name == "Archer")
            {
                archer = PlayerInfo.Instance.players[i];
            }
        }
        archerScript = archer.GetComponent<Archer>();
    }

    public void ChangeMode()
    {
        if (archerScript.meleeMode) archerScript.meleeMode = false;
        else archerScript.meleeMode = true;
    }
    public void OnClickColor()
    {
        if (!toggle)
        {
            buttonImage.color = toggleColor;
            toggle = true;
        }
        else
        {
            buttonImage.color = originalColor;
            toggle = false;
        }
    }
}
