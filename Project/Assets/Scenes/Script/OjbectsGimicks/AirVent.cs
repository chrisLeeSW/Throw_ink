using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirVent : MonoBehaviour
{

    public float upHitDistance = 10f;
    private float jumpForce = 0.5f;
    private float jumpForceDefault = 0.5f;
    public float increaseJumpForce = 0.5f;
    
    private void FixedUpdate()
    {
        Vector3 boxSize = transform.localScale / 2;

        RaycastHit hit;
        if(Physics.BoxCast(transform.position, boxSize,Vector3.up , out hit , Quaternion.identity, upHitDistance))
        {
            Debug.Log(hit.collider.tag);
            if(hit.collider.tag == "Player")
            {
                var t = hit.collider.GetComponent<PlayerMoveMent>();
                t.JumpCollisionByPad(jumpForce,1);
                jumpForce += increaseJumpForce;
                if(jumpForce >= upHitDistance/2)
                {
                    jumpForce=upHitDistance/2;
                }
            }
        }
        else
        {
            jumpForce = jumpForceDefault;
        }
    }
}
