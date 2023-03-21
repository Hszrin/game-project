using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusPanelHp : MonoBehaviour
{
    public Slider hp;
    public StatusPanel statusPanel;
    private TextMeshProUGUI hpText;
    private ChracterComponent chracter;
    private void Start()
    {
        hpText = transform.GetComponent<TextMeshProUGUI>();
        hp = transform.parent.GetComponent<Slider>();
        chracter = statusPanel.chracterComponent;
    }
    private void Update()
    {
        hpText.text = string.Format("{0}/{1}", chracter.maxHp.ToString(), chracter.currentHp.ToString());
        hp.value = chracter.currentHp / chracter.maxHp;
    }
}
