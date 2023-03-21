using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceANatureAmmo : MonoBehaviour
{
    public List<Image> ammo = new List<Image>();
    public List<Color> ammoColor = new List<Color>();
    public int curColorIdx;
    public void ChangeAmmoColor(Index idx)
    {
        ammo[idx.index].enabled = false;
    }
    public void Reload()
    {
        for (int i = 0; i <= 2; i++)
        {
            ammo[i].enabled = true;
        }
        ammo[2].color = ammoColor[curColorIdx];
    }
}
