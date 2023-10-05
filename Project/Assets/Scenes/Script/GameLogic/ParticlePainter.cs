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
        //    Debug.LogError("브러시가 설정되지 않았습니다.");
        //    return;
        //}

        //if (canvas == null)
        //{
        //    Debug.LogError("InkCanvas가 설정되지 않았습니다.");
        //    return;
        //}

        //// InkCanvas에 브러시로 그림을 그립니다.
        //if (canvas.Paint(brush, position))
        //{
        //    Debug.Log("성공적으로 그림을 그렸습니다.");
        //}
        //else
        //{
        //    Debug.LogWarning("그림을 그리지 못했습니다.");
        //}
    }
}
