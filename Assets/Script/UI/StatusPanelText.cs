using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class StatusPanelText : MonoBehaviour, IPointerEnterHandler
{
    public TextMeshProUGUI textMeshProUGUI;
    private TextComponent textComponent = new TextComponent();


    public void OnPointerEnter(PointerEventData eventData) 
    {
        Debug.Log("!");
    }
    public void AssignComponent(string text, float amount, string content = null)
    {
        textComponent.text = text;
        textComponent.amount = amount;
        textComponent.content = content;
        textMeshProUGUI.text = string.Format("{0}:{1}", textComponent.text, textComponent.amount);
        gameObject.SetActive(true);
    }
    public class TextComponent
    {
        public string text;
        public float amount;
        public string content;
    }
}
