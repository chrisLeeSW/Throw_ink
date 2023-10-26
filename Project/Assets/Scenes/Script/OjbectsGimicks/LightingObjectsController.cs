using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingObjectsController : MonoBehaviour
{
    public ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();    
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {

            Debug.Log($"Die & {other.gameObject.name}");

        }
    }

}
