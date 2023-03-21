using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChracterSlot : MonoBehaviour, IPointerClickHandler
{
    private ChracterProfileStatusGroup profileStatusGroupScript;
    public GameObject profileStatusGroup;
    public Image currentSlotImage;
    public Transform assignedChracter;
    public ChracterComponent chracterComponent;
    public GameObject deadSign;
    private UIManager uiManager;
    private ClickManager clickManager;
    public int slot;

    float interval = 0.25f;
    float doubleClickedTime = -1.0f;
    bool isDoubleClicked = false;

    public Button prefabButton;
    public Button button;
    private void Start()
    {
        assignedChracter = PlayerInfo.Instance.players[slot].transform;
        chracterComponent = assignedChracter.GetComponent<IChracterComponent>().ReturnChracterComponent();
        uiManager = transform.parent.GetComponent<UIManager>();
        profileStatusGroup = uiManager.profileStatusGroup.gameObject;
        clickManager = GameObject.Find("ClickManager").GetComponent<ClickManager>();
    }
            
    public void OnPointerClick(PointerEventData eventData)
    { 
        profileStatusGroup.SetActive(true);
        profileStatusGroupScript = profileStatusGroup.GetComponent<ChracterProfileStatusGroup>();
        profileStatusGroupScript.chracterImage.sprite = currentSlotImage.sprite;

        if (currentSlotImage.name == "Archer")
        {
            button = Instantiate(prefabButton, profileStatusGroup.transform);
        }

        if (assignedChracter != null) profileStatusGroupScript.Hp.value = chracterComponent.hpSlider.value;
        else
        {
            profileStatusGroupScript.Hp.value = 0;
            profileStatusGroupScript.deadSign.SetActive(true);
        }

        if ((Time.time - doubleClickedTime) < interval)
        {
            isDoubleClicked = true;
            doubleClickedTime = -1.0f;

            clickManager.DoubleClick(chracterComponent.chracter.transform);
        }
        else
        {
            isDoubleClicked = false;
            doubleClickedTime = Time.time;
        }
    }
    public void UpdateHp()
    {
        if(profileStatusGroupScript != null)
        {
            profileStatusGroupScript.Hp.value = chracterComponent.hpSlider.value;
        }
    }
    public void UpdateDead()
    {
        if (profileStatusGroupScript != null)
        {
            profileStatusGroupScript.deadSign.SetActive(true);
        }
        assignedChracter = null;
        deadSign.SetActive(true);
    }
}
