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
    private PlayerSpecialStatList assignedPlayerSpecialStatList;
    private List<TextMeshProUGUI> statListText = new List<TextMeshProUGUI>();
    private List<GameObject> statList = new List<GameObject>();
    private List<TextMeshProUGUI> skillList = new List<TextMeshProUGUI>();
    public Image currentSlotImage;
    private void Awake()
    {
        skillListTransform = transform.Find("SkillList");
        statListTransform = transform.Find("StatList");
        for (int i = 0; i < 10; i++)
        {
            statListText.Add(statListTransform.GetChild(i).GetComponent<TextMeshProUGUI>());
            statList.Add(statListTransform.GetChild(i).gameObject);
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
        assignedPlayerSpecialStatList = assignedChracter.GetComponent<IPlayerSpecialStatList>().ReturnPlayerSpecialStatList();
        currentSlotImage.sprite = chracterComponent.chraceterImage;
        statListText[0].text = string.Format("공격력:{0}", chracterComponent.power);
        statListText[1].text = string.Format("무게:{0}", chracterComponent.weight);
        statListText[2].text = string.Format("속도:{0}", chracterComponent.speed);
        statListText[3].text = string.Format("최대체력:{0}", chracterComponent.maxHp);
        statListText[4].text = string.Format("치명타 확률:{0}", chracterComponent.critChance);
        for(int i = 0;i < assignedPlayerSpecialStatList.specialStatList.Count; i++)
        {
            statListText[5 + i].text = string.Format("{0}:{1}", assignedPlayerSpecialStatList.ReturnStatName(i), assignedPlayerSpecialStatList.ReturnAmount(i));
            statList[5 + i].SetActive(true);
        }
    }
    
    public void DeActivePanel()
    {
        for (int i = 5; i < 10; i++)
        {
            statList[i].SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
