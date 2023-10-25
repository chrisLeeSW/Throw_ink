using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class VirtualJoystick2Test : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
    ,IDragHandler
{

    public Vector2 Value { get; private set; }

    private int pointerId;
    private bool isDragging;

    public void OnDrag(PointerEventData eventData)
    {
        //if(pointerId != eventData.pointerId)
        Value = eventData.delta / Screen.dpi;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Value = eventData.delta / Screen.dpi;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Value = Vector2.zero;
    }
}
