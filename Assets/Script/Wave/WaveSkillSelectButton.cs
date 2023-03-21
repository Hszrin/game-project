using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveSkillSelectButton : MonoBehaviour
{
    Transform wavePanel;
    WavePanel wavePanelScript;
    public SkillInfo skillInfo;
    public TextMeshProUGUI skillText;
    public TextMeshProUGUI skillName;
    private void OnEnable()
    {
        wavePanel = transform.parent;
        wavePanelScript = wavePanel.GetComponent<WavePanel>();
    }
    public void OnPointClick()
    {
        wavePanelScript.currentSkillInfo = skillInfo;
    }
    public void ChangeText()
    {
        skillText.text = skillInfo.skillText;
        skillName.text = skillInfo.name;
    }
}
