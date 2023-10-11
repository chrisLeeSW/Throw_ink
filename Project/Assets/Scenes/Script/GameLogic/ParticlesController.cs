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

    private void FixedUpdate()
    {
        ++waitCount;
    }
    private void OnParticleCollision(GameObject other)
    {
        if (waitCount < wait)
            return;
        waitCount = 0;

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
