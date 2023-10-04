using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static PaintingObject;

public class PaintingObject : MonoBehaviour
{
    public class PaintSet
    {
        public Material material;
        public bool useMainPaint = true;
        public Texture mainTexture;
        public RenderTexture paintMainTexture;

        public PaintSet() { }

        public PaintSet(bool useMainPaint)
        {
            this.useMainPaint = useMainPaint;
        }
        public PaintSet(bool useMainPaint, Material material): this(useMainPaint)
        {
            this.material = material;
        }
    }
    private bool eraseFlag;
    private event Action<PaintingObject, MyBrush> OnPaintStart;
    private List<PaintSet> paintset;

    public bool Paint(MyBrush brush, RaycastHit hitInfo, Func<PaintSet, bool> materialSelector = null)
    { 
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider is MeshCollider)
                return PaintUVDirect(brush, hitInfo.textureCoord, materialSelector);
            Debug.LogWarning("If you want to paint using a RaycastHit, need set MeshCollider for object.");
        }
        return false;
    }

    private bool PaintUVDirect(MyBrush brush , Vector2 hit, Func<PaintSet, bool> material = null)
    {

        if (brush == null)
        {
            Debug.LogError("Do not set the brush.");
            eraseFlag = false;
            return false;
        }

        if (OnPaintStart != null)
        {
            brush = brush.Clone() as MyBrush;
            OnPaintStart(this, brush);
        }

        var set = material == null ? paintset : paintset.Where(material);
        foreach (var p in set)
        {
            var mainPaintConditions = p.useMainPaint && brush.BrushTexture != null && p.paintMainTexture != null && p.paintMainTexture.IsCreated();
            //if (eraseFlag)
            //    brush = GetEraser(brush, p, hit, mainPaintConditions, normalPaintConditions, heightPaintConditions);

        //    if (mainPaintConditions)
        //    {
        //        var mainPaintTextureBuffer = RenderTexture.GetTemporary(p.paintMainTexture.width, p.paintMainTexture.height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
        //        SetPaintMainData(brush, uv);
        //        Graphics.Blit(p.paintMainTexture, mainPaintTextureBuffer, paintMainMaterial);
        //        Graphics.Blit(mainPaintTextureBuffer, p.paintMainTexture);
        //        RenderTexture.ReleaseTemporary(mainPaintTextureBuffer);
        //    }
        }


            return false;
    }

    public bool Erase(MyBrush brush, RaycastHit hitInfo, Func<PaintSet, bool> materialSelector = null)
    {
        eraseFlag = true;
        return Paint(brush, hitInfo, materialSelector);
    }
}
