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
            Vector3 paintPosition = collisionEvents[0].intersection; // ù ��° �浹 ����
            InkCanvas canvas = other.GetComponent<InkCanvas>();

            if (canvas != null)
            {
                p.Paint(canvas, paintPosition);
            }
        }

        //InkCanvas canvas = other.GetComponent<InkCanvas>();
        //if (canvas != null)
        //{
        //    // ��ƼŬ�� �浹�� ��Ȯ�� ��ġ�� �������� ���� ������ �ʿ��մϴ�.
        //    // ���������� ������Ʈ�� �߽��� ����մϴ�.
        //    Vector3 paintPosition = other.transform.position;
        //    p.Paint(canvas, paintPosition);
        //}
        // Debug.Log("Particle collided with: " + other.name);
    }
}
