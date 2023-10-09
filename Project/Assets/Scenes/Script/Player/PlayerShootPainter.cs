using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShootPainter : MonoBehaviour
{
    protected enum GunType
    {
        NormalGun,
    } 

    public ParticleSystem particle;
    public Transform gunPivot;
    private GunType gunType =GunType.NormalGun;
    private bool isShooting;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isShooting = !isShooting;

            if(isShooting)
                particle.Play();
            else if (!isShooting)
                particle.Stop();
        }
    }

    public bool GetIsShooting()
    {
        return isShooting;  
    }    
}
