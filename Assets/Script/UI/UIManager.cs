using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Transform profileStatusGroup;
    private void Awake()
    {
        profileStatusGroup = transform.Find("StatusGroup");
    }
    public void EnablePanel()
    {
        gameOverPanel.SetActive(true);
    }
}
