using UnityEngine;

public class Test : MonoBehaviour
{
    public Color targetColor = Color.red;  // Set the target color
    public float tolerance = 0.2f;  // Set a tolerance value for color comparison

    private float time;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            GetColorRatio();
        }
        //time += Time.deltaTime;

        //if (time > 1.0f)
        //{
        //    GetColorRatio();
        //    time = 0f;
        //}
        //if (Input.GetMouseButton(0))
        //{
        //    GetColorRatio();
        //}
    }

    float GetColorRatio()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        var renderTexture = GetComponent<Renderer>().material.mainTexture as RenderTexture;

        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        // Set the current RenderTexture as active
        RenderTexture currentActiveRT = RenderTexture.active;
        RenderTexture.active = renderTexture;
        // Read the pixels from the RenderTexture into the Texture2D
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();
        // Restore the previous active RenderTexture
        RenderTexture.active = currentActiveRT;

        int targetColorCount = 0;
        int totalColorCount = 0;

        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector2 uv0 = mesh.uv[triangles[i]];
            Vector2 uv1 = mesh.uv[triangles[i + 1]];
            Vector2 uv2 = mesh.uv[triangles[i + 2]];

            for (float alpha = 0; alpha <= 1; alpha += 0.01f)
            {
                for (float beta = 0; beta <= 1 - alpha; beta += 0.01f)
                {
                    float gamma = 1 - alpha - beta;
                    Vector2 uv = alpha * uv0 + beta * uv1 + gamma * uv2;

                    Color pixelColor = texture.GetPixelBilinear(uv.x, uv.y);
                    totalColorCount++;

                    if (IsColorMatch(pixelColor, targetColor, tolerance))
                    {
                        //Debug.Log(pixelColor);
                        targetColorCount++;
                    }
                }
            }
        }

        float colorRatio = (float)targetColorCount / totalColorCount;
        Debug.Log("Target Color Ratio: " + colorRatio);
        return colorRatio;
    }

    bool IsColorMatch(Color color1, Color color2, float tolerance)
    {
        float rDiff = Mathf.Abs(color1.r - color2.r);
        float gDiff = Mathf.Abs(color1.g - color2.g);
        float bDiff = Mathf.Abs(color1.b - color2.b);

        return rDiff <= tolerance && gDiff <= tolerance && bDiff <= tolerance;
    }
}

