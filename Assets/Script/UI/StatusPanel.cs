using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusPanel : MonoBehaviour
{
    private Transform assignedChracter;
    private Transform skillListTransform;
    private Transform statListTransform;
    public ChracterComponent chracterComponent;
    private List<TextMeshProUGUI> statList = new List<TextMeshProUGUI>();
    private List<TextMeshProUGUI> skillList = new List<TextMeshProUGUI>();
    public Image currentSlotImage;
    private void Awake()
    {
        skillListTransform = transform.Find("SkillList");
        statListTransform = transform.Find("StatList");
        for (int i = 0; i < 10; i++)
        {
            statList.Add(statListTransform.GetChild(i).GetComponent<TextMeshProUGUI>());
        }
        for (int i = 0; i < 12; i++)
        {
            skillList.Add(skillListTransform.GetChild(i).GetComponent<TextMeshProUGUI>());
        }
    }

    public void AssignChracter(Transform chracter)
    {
        assignedChracter = chracter;
        chracterComponent = assignedChracter.GetComponent<IChracterComponent>().ReturnChracterComponent();
        currentSlotImage.sprite = chracterComponent.chraceterImage;
        statList[0].text = string.Format("공격력:{0}", chracterComponent.power); 
        statList[1].text = string.Format("무게:{0}", chracterComponent.weight); 
        statList[2].text = string.Format("속도:{0}", chracterComponent.speed); 
        statList[3].text = string.Format("최대체력:{0}", chracterComponent.maxHp); 
        statList[4].text = string.Format("치명타 확률:{0}", chracterComponent.critChance);
    }
    
}
