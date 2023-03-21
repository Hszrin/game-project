using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System;

public class WavePanel : MonoBehaviour
{
    public List<Button> skillButtons = new List<Button>();

    public UnityEvent onPanelEnableEvent;
    public UnityEvent onPanelDisableEvent;
    public SkillInfo currentSkillInfo;
    public ChracterComponent IC;
    public WaveController waveController;
    public int slot = 0;

    private void OnEnable()
    {
        waveController = GameObject.Find("WaveController").GetComponent<WaveController>();
        onPanelDisableEvent.AddListener(waveController.InstantiateChracter);
        if (PlayerInfo.Instance.players[slot] == null) slot++;
        onEnablePanel();
    }

    public void onEnablePanel()
    {
        string chracter = null;
        if (slot == 0) chracter = "KnightSkillInfo";
        else if (slot == 1) chracter = "ArcherSkillInfo";
        string path = string.Format("Json/Wave{0}/{1}", waveController.waveRound - 1, chracter);
        TextAsset jsonTextFile = Resources.Load(path) as TextAsset;
        SkillList skillList = JsonUtility.FromJson<SkillList>(jsonTextFile.ToString());
        List<int> idx = new List<int>();

        if (skillList.skillInfo.Count == 3)
        {
            idx.Add(0);
            idx.Add(1);
            idx.Add(2);
        }
        else
        {
            while (idx.Count < 3)
            {
                int num = UnityEngine.Random.Range(0, skillList.skillInfo.Count);
                if (!idx.Contains(num)) idx.Add(num);
            }
        }

        for (int i = 0; i < 3; i++)
        {
            skillButtons[i].GetComponent<WaveSkillSelectButton>().skillInfo = skillList.skillInfo[idx[i]];
        }
        onPanelEnableEvent.Invoke();
    }

    public void WavePanelManager()//스킬 버튼 클릭했을때
    {
        AssignSkill();
        if (slot == 0)
        {
            slot++;
            if (PlayerInfo.Instance.players[slot] == null)
            {
                gameObject.SetActive(false);
                onPanelDisableEvent.Invoke();
            }
            else onEnablePanel();
        }
        else
        {
            gameObject.SetActive(false);
            onPanelDisableEvent.Invoke();
            slot = 0;
        }
    }

    public void AssignSkill()
    {
        ChracterComponent IC = PlayerInfo.Instance.players[slot].GetComponent<IChracterComponent>().ReturnChracterComponent();
        if (currentSkillInfo.type == "typeStat")
        {
            switch (currentSkillInfo.name)
            {
                case "최대 체력 증가":
                    IC.MaxHpAdjust(IC.currentHp + float.Parse(currentSkillInfo.amount), IC.maxHp + float.Parse(currentSkillInfo.amount));
                    IC.skillList.Add("최대 체력 증가");
                    break;
                case "공격력 증가":
                    IC.power += float.Parse(currentSkillInfo.amount);
                    IC.skillList.Add("공격력 증가");
                    break;
                case "체력 회복":
                    IC.HpAdjust(float.Parse(currentSkillInfo.amount));
                    break;
                case "무게 증가":
                    IC.weight += float.Parse(currentSkillInfo.amount);
                    IC.skillList.Add("무게 증가");
                    break;
                case "속도 증가":
                    IC.speed += float.Parse(currentSkillInfo.amount);
                    IC.skillList.Add("속도 증가");
                    break;
                case "치명타 확률 증가":
                    IC.critChance += float.Parse(currentSkillInfo.amount);
                    IC.skillList.Add("치명타 확률 증가");
                    break;
            }
        }
        else
        {
            PlayerInfo.Instance.players[slot].AddComponent(Type.GetType(currentSkillInfo.componentName));
        }
    }
}
[System.Serializable]
public class SkillInfo
{
    public string name;
    public string skillText;
    public string type; //typeComponent, typeStat
    public string amount;
    public string componentName;
}

[System.Serializable]
public class SkillList
{
    public List<SkillInfo> skillInfo = new List<SkillInfo>();
}