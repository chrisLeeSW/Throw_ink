using Es.InkPainter;
using Es.InkPainter.Effective;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{

    public ParticlePainter p;
    public ParticleSystem particle;
    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private int wait = 3;
    private int waitCount;

    public Material material;

    private void Awake()
    {
        material.color = OnGameData.instance.gameColor;
        //ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
        //var color = particle.GetComponent<ParticleSystemRenderer>();
        //color.material.color = OnGameData.instance.gameColor;
    }
    private void FixedUpdate()
    {
        ++waitCount;
    }
    private void OnParticleCollision(GameObject other)
    {
        if (waitCount < wait)
            return;
        waitCount = 0;

        if (other.CompareTag("InkCube"))
        {
            particle.GetCollisionEvents(other, collisionEvents);
            if (collisionEvents.Count > 0)
            {
                Vector3 paintPosition = collisionEvents[0].intersection;
                InkCanvas canvas = other.GetComponent<InkCanvas>();
                if (canvas != null)
                {
                    p.Paint(canvas, paintPosition);
                }
            }
        }
    }
}
