using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeCatapultController : MonoBehaviour
{
    public float knockbackForce = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            var t = collision.collider.GetComponent<PlayerMoveMent>();
            Vector3 playerDirection = t.transform.forward;
            t.PlayerNuckBackForward(playerDirection, knockbackForce);
        }
    }
}
