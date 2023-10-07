using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class UiGameManager : MonoBehaviour
{

    public Joystick joystick;

    public void OnTouchBridge(PointerEventData eventData)
    {
        joystick.OnTouch();
    }

    public void OnDragBridge()
    {
        joystick.OnDrag();
    }

    public void OffTouchBridge()
    {
        joystick.OffTouch();
    }


    [Serializable]
    public class Joystick
    {
        public RectTransform leverMaxTransform;
        public RectTransform lever;

        public  void OnTouch()
        {
            Debug.Log("Touch");
        }

        public void OnDrag( )
        {
         
            Debug.Log("Drag");
        }

        public void OffTouch()
        {
            lever.anchoredPosition = Vector2.zero; 
            Debug.Log("OffTouch");
        }

    }

}
