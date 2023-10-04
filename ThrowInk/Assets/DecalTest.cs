using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalTest : MonoBehaviour
{
    public Material decalMaterial; // ��Į ���׸����� ���⿡ �巡�� �� ���

    private int colorIndex = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ��
        {
            ChangeColor();
        }
    }

    void ChangeColor()
    {
        colorIndex++;

        if (colorIndex > 3) // 4�� Ŭ������ ��, �ʱ�ȭ
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
// ���׸����� ä���ϴ� ����    