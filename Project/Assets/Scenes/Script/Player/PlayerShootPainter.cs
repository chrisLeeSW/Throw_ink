using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShootPainter : MonoBehaviour
{
    private enum GunType
    {
        NormalGun,
    } // gunType으로 컴퍼먼트 가져 올 수 있게 해볼 예정

    NormalGun gun;

    private void Awake()
    {
        gun = GetComponent<NormalGun>();   
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gun.Fire();
        }
    }
}
