using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseHandler : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public MouseEditorState mouse;
    public int tempPrefab;

    void Start()
    {

        mouse = GameObject.FindObjectOfType<MouseEditorState>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        tempPrefab = mouse.currentPrefab;
        mouse.currentPrefab = 99;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse.currentPrefab = tempPrefab;

    }
   

}
