using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObjectsController : MonoBehaviour
{
    public float knockbackForce = 50f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            var t = collision.collider.GetComponent<Rigidbody>();
           t.AddForce(transform.forward * knockbackForce);  
        }
    }
}
