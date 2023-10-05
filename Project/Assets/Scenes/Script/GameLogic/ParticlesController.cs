using Es.InkPainter;
using Es.InkPainter.Effective;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{

    public ParticlePainter p;
    public ParticleSystem particle;
    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private void OnParticleCollision(GameObject other)
    {
        particle.GetCollisionEvents(other, collisionEvents);
        if (collisionEvents.Count > 0)
        {
            Vector3 paintPosition = collisionEvents[0].intersection; // 첫 번째 충돌 지점
            InkCanvas canvas = other.GetComponent<InkCanvas>();

            if (canvas != null)
            {
                p.Paint(canvas, paintPosition);
            }
        }
    }
}
