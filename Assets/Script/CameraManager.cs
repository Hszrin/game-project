using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Camera cam;
    private float z = 0;
    private float x = 0;
    private float valx = 0;
    private bool zoomed = false;
    private bool zoomIn = false;
    private bool zoomOut = false;
    private bool changePos = false;
    private bool coroutine = false;
    private ClickManager clickManager;
    private GameObject curDragPoint;
    Vector3 vec = new Vector3(0f, 28f, 0f);
    private void Start()
    {
        cam = transform.GetComponent<Camera>();
        clickManager = GameObject.Find("ClickManager").GetComponent<ClickManager>();
    }
    private void Update()
    {
        if (zoomIn == true)
        {
            if (coroutine == false) StartCoroutine(EndCameraZoom());
            vec.x += Time.deltaTime * (x - vec.x) * 7f;
            vec.z += Time.deltaTime * (z - vec.z) * 7f;
            transform.position = vec;
            valx += Time.deltaTime * (1f - valx) * 10f;
            cam.orthographicSize = 16.55f - 11.55f * Mathf.Pow(valx, 7f);
        }
        if (zoomOut == true)
        {
            if (coroutine == false) StartCoroutine(EndCameraZoom());
            vec = transform.position;
            vec.x -= Time.deltaTime * vec.x * 7f;
            vec.z -= Time.deltaTime * vec.z * 7f;
            transform.position = vec;
            valx += Time.deltaTime * (1f - valx) * 10f;
            cam.orthographicSize = 16.55f - 11.55f * (1f - Mathf.Pow(valx, 7f));
        }
        if (changePos == true)
        {
            if (coroutine == false) StartCoroutine(EndCameraMove());
            vec.x += Time.deltaTime * (x - vec.x) * 7f;
            vec.z += Time.deltaTime * (z - vec.z) * 7f;
            transform.position = vec;
        }
    }
    IEnumerator EndCameraZoom()
    {
        coroutine = true;
        clickManager.clickLock = true;
        yield return new WaitForSeconds(0.6f);
        clickManager.clickLock = false;
        coroutine = false;
        zoomIn = false;
        zoomOut = false;
        changePos = false;
        zoomed = zoomed ? false : true;
        valx = 0f;
    }
    IEnumerator EndCameraMove()
    {
        coroutine = true;
        clickManager.clickLock = true;
        yield return new WaitForSeconds(0.7f);
        clickManager.clickLock = false;
        coroutine = false;
        changePos = false;
        valx = 0f;
    }
    public void CameraMoveFunc(Transform pos) {
        if (zoomed) ChangePos(pos);
        else ZoomInPos(pos);
    }
    public void ZoomInPos(Transform pos)
    {
        if (zoomIn == false && zoomOut == false && changePos == false && zoomed == false)
        {
            curDragPoint = pos.Find("DragPoint").gameObject;
            curDragPoint.SetActive(false);
            zoomIn = true;
            x = pos.position.x + 4f;
            z = pos.position.z;
        }
    }
    public void ChangePos(Transform pos)
    {
        if (zoomIn == false && zoomOut == false && changePos == false && zoomed == true)
        {
            changePos = true;
            curDragPoint.SetActive(true);
            curDragPoint = pos.Find("DragPoint").gameObject;
            curDragPoint.SetActive(false);
            x = pos.position.x + 4f;
            z = pos.position.z;
        }
    }
    public void ResetPos(GameObject statusPanel)
    {
        if (zoomIn == false && zoomOut == false && changePos == false && zoomed == true)
        {
            zoomOut = true;
            curDragPoint.SetActive(true);
            statusPanel.SetActive(false);
        }
    }
}