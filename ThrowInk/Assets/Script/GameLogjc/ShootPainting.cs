using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootPainting : MonoBehaviour
{
    [SerializeField]
    private MyBrush brush;

    [SerializeField]
    bool erase = false;

    private void Awake()
    {
        brush.Color = Color.white;
    }
    private enum HitMethodType
    {
        Raycast,
        // �浹 �˻� �κ� �߰� -> �޼ҵ� Ȯ�� �ʿ�
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool success = true;
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                var paintObject = hitInfo.transform.GetComponent<PaintingObject>();
                if (paintObject != null)
                {
                   success = erase ? paintObject.Erase(brush, hitInfo) : paintObject.Paint(brush, hitInfo);
                }
                if (!success)
                    Debug.LogError("Failed to paint.");
            }

        }
    }
}
