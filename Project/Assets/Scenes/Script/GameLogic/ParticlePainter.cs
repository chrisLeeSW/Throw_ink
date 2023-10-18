using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter;

public class ParticlePainter : MonoBehaviour
{
    [SerializeField]
    Brush brush = null;

    private void Awake()
    {
        if(brush != null)
        {
            brush.Color = OnGameData.instance.gameColor;
        }
    }
    public void Paint(InkCanvas canvas, Vector3 position)
    {
        canvas.Paint(brush, position);
    }
}
