using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalTest : MonoBehaviour
{
    public Material decalMaterial; // 데칼 메테리얼을 여기에 드래그 앤 드롭

    private int colorIndex = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {
            ChangeColor();
        }
    }

    void ChangeColor()
    {
        colorIndex++;

        if (colorIndex > 3) // 4번 클릭했을 때, 초기화
        {
            colorIndex = 0;
        }

        switch (colorIndex)
        {
            case 0:
                decalMaterial.color = Color.white;
                break;
            case 1:
                decalMaterial.color = Color.red;
                break;
            case 2:
                decalMaterial.color = Color.green;
                break;
            case 3:
                decalMaterial.color = Color.blue;
                break;
        }
    }
}
// 메테리얼을 채색하는 형식    