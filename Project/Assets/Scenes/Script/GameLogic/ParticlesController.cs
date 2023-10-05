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

        //InkCanvas canvas = other.GetComponent<InkCanvas>();
        //if (canvas != null)
        //{
        //    // 파티클과 충돌한 정확한 위치를 가져오기 위한 로직이 필요합니다.
        //    // 예제에서는 오브젝트의 중심을 사용합니다.
        //    Vector3 paintPosition = other.transform.position;
        //    p.Paint(canvas, paintPosition);
        //}
        // Debug.Log("Particle collided with: " + other.name);
    }
}
