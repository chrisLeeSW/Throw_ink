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
        // 충돌 검사 부분 추가 -> 메소드 확인 필요
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
