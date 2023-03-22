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
    private List<StatusPanelText> statListText = new List<StatusPanelText>();
    private List<TextMeshProUGUI> skillList = new List<TextMeshProUGUI>();
    public Image currentSlotImage;
    private void Awake()
    {
        skillListTransform = transform.Find("SkillList");
        statListTransform = transform.Find("StatList");
        for (int i = 0; i < 10; i++)
        {
            statListText.Add(statListTransform.GetChild(i).GetComponent<StatusPanelText>());
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
        statListText[0].AssignComponent("���ݷ�", chracterComponent.power);
        statListText[1].AssignComponent("����", chracterComponent.weight);
        statListText[2].AssignComponent("�ӵ�", chracterComponent.speed);
        statListText[3].AssignComponent("�ִ�ü��", chracterComponent.maxHp);
        statListText[4].AssignComponent("ġ��Ÿ Ȯ��", chracterComponent.critChance);
        for(int i = 0;i < assignedPlayerSpecialStatList.specialStatList.Count; i++)
        {
            statListText[5 + i].AssignComponent(assignedPlayerSpecialStatList.ReturnStatName(i), assignedPlayerSpecialStatList.ReturnAmount(i));
        }
    }
}
