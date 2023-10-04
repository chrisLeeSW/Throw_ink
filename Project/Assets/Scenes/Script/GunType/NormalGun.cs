using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGun : AbstractGun
{
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Fire();
        }    
        if(Input.GetMouseButtonUp(0))
        {
            particle.Stop();
        }
    }
    public override void Fire()
    {
        particle.Play();
    }

    
}
