using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{


    public enum Axis
    {
        Horizontal,
        Vertical
    }

    public Image stick;
    public float radOffset;
    private float radius;//����ȭ �ݰ� ��� ���
    private Vector3 originalPoint = Vector3.zero;
    private RectTransform rectTr;

    private Vector2 value;

    private int pointerId;
    private bool isDragging;

    private void Awake()
    {
        rectTr = GetComponent<RectTransform>();
        originalPoint = stick.rectTransform.position; // ��Ŀ�� ���� �����ǵ��� ����
        radius = rectTr.sizeDelta.x / 2 - radOffset;


    }

    public float GetAxis(Axis axis)
    {
        switch (axis)
        {
            case Axis.Horizontal:
                return value.x;
            case Axis.Vertical:
                return value.y;
        }
        return 0f;
    }

   
    
    private void UpdateStickPos(Vector3 screenPos)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            rectTr, screenPos, null, out Vector3 newPoint);

        var delta = Vector3.ClampMagnitude(newPoint - originalPoint, radius);

        //value = delta.normalized;
        value = delta / radius; 
        stick.rectTransform.position = originalPoint + delta;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isDragging)
            return;
        isDragging = true;
        pointerId= eventData.pointerId;

        UpdateStickPos(eventData.position);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (pointerId != eventData.pointerId)
            return;

        UpdateStickPos(eventData.position);
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if (pointerId != eventData.pointerId)
            return;

        isDragging = false;
        UpdateStickPos(originalPoint);
        //stick.rectTransform.position = originalPoint;
        //value = Vector2.Zero;
    }
}
