using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameEndScript : MonoBehaviour
{
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("SampleGame");
    }
}
