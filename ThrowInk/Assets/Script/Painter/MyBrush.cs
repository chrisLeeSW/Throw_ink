using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class MyBrush 
{
    [SerializeField]
    private Color brushColor;
    [SerializeField, Range(0, 1)]
    private float brushScale = 0.1f;
    [SerializeField, Range(0, 360)]
    private int brushAngel = 0;
    [SerializeField]
    private Texture brushTexture;

    public Color Color
    {
        get { return brushColor; }
        set { brushColor = value; }
    }
    public float Scale
    {
        get { return Mathf.Clamp01(brushScale); }
        set { brushScale = Mathf.Clamp01(value); }
    }
    public int BrushAngel
    {
        get { return brushAngel; }
        set { brushAngel = value; }
    }
    public Texture BrushTexture
    {
        get { return brushTexture; }
        set { brushTexture = value; }
    }
    public MyBrush(Texture brushTex, float scale, Color color)
    {
        BrushTexture = brushTex;
        Scale = scale;
        Color = color;
    }

    public object Clone()
    {
        return MemberwiseClone();
    }

}
