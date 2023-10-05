using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShootPainter : MonoBehaviour
{
    private enum GunType
    {
        NormalGun,
    } // gunType���� ���۸�Ʈ ���� �� �� �ְ� �غ� ����

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
