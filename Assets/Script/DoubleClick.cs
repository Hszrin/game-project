using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleClick : MonoBehaviour
{
    float interval = 0.25f;
    float doubleClickedTime = -1.0f;
    bool isDoubleClicked = false;
    private ClickManager clickManager;
    public Transform pos;
    private void Start()
    {
        clickManager = GameObject.Find("ClickManager").GetComponent<ClickManager>();
    }

    private void OnMouseUp()
    {
        if ((Time.time - doubleClickedTime) < interval)
        {
            isDoubleClicked = true;
            doubleClickedTime = -1.0f;
        }
        else
        {
            isDoubleClicked = false;
            doubleClickedTime = Time.time;
        }
    }

    void Update()
    {
        if (isDoubleClicked)
        {
            clickManager.DoubleClick(pos);
            isDoubleClicked = false;
        }
    }
}
