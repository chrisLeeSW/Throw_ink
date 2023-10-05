using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter;

public class ParticlePainter : MonoBehaviour
{
    [SerializeField]
    Brush brush = null;

     public int wait = 3;
    private int waitCount;
    private void Awake()
    {
        brush.Scale = 0.1f;
    }
    public void Paint(InkCanvas canvas, Vector3 position)
    {
        //if (waitCount > wait)
        //    return;
        //waitCount ++;


        Debug.Log("Particle");
        Debug.Log(position);
        canvas.Paint(brush, position);
        Debug.Log("Print");

        //if (brush == null)
        //{
        //    Debug.LogError("�귯�ð� �������� �ʾҽ��ϴ�.");
        //    return;
        //}

        //if (canvas == null)
        //{
        //    Debug.LogError("InkCanvas�� �������� �ʾҽ��ϴ�.");
        //    return;
        //}

        //// InkCanvas�� �귯�÷� �׸��� �׸��ϴ�.
        //if (canvas.Paint(brush, position))
        //{
        //    Debug.Log("���������� �׸��� �׷Ƚ��ϴ�.");
        //}
        //else
        //{
        //    Debug.LogWarning("�׸��� �׸��� ���߽��ϴ�.");
        //}
    }
}
