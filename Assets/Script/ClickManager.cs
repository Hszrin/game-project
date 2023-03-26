using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickManager : MonoBehaviour
{
    public CameraManager cameraManager;
    public GameObject statusPanel;
    public UnityEvent doubleClickChracterEvent;
    private Transform pos;
    public bool clickLock = false;
    private void Start()
    {
        cameraManager = GameObject.Find("Main Camera").GetComponent<CameraManager>();
        doubleClickChracterEvent.AddListener(() => cameraManager.CameraMoveFunc(pos));
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) cameraManager.ResetPos(statusPanel);
    }

    public void DoubleClick(Transform pos)
    {
        if (!clickLock)
        {
            this.pos = pos;
            statusPanel.SetActive(true);
            statusPanel.GetComponent<StatusPanel>().AssignChracter(pos);
            doubleClickChracterEvent.Invoke();
        }
    }
}
