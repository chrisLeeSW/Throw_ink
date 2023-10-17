using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCode : MonoBehaviour
{

    //설정한 색 변경할 수있게 작업 진행
    private MeshRenderer mesh;
    private Color testColor;
    int rvalue;
    int gvalue;
    int bvalue;
    private void Awake()
    {
        testColor = new Color(0, 0, 0, 255);
        mesh = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            mesh.material.color = testColor;
        }
        if(Input.GetKey(KeyCode.F2))
        {
            rvalue++;
            testColor.r = rvalue/255f;
        }
        if(Input.GetKey(KeyCode.F3))
        {
            gvalue++;
            testColor.g = gvalue/255f;
        }
        if(Input.GetKey(KeyCode.F4))
        {
            bvalue++;
            testColor.b = bvalue / 255f;
        }
        if(Input.GetKeyDown(KeyCode.F5))
        {
            if (testColor.r > 0)
                testColor.r--;
        }
        if(Input.GetKeyDown(KeyCode.F6))
        {
            if(testColor.g > 0)
                testColor.g--;
        }
        if(Input.GetKeyDown(KeyCode.F7))
        {
            if(testColor.b > 0)
                testColor.b--;
        }
        if(Input.GetKeyDown(KeyCode.F8))
        {
            testColor.r = 255;
        }

        if(Input.GetKeyDown(KeyCode.F12))
        {
            Debug.Log($"r:{testColor.r}/g:{testColor.g}/b:{testColor.b}");
        }
    }

}
