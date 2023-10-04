using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractGun : MonoBehaviour
{
    public ParticleSystem particle;
    public Transform gunPivot;
    // public AudioSource audio;
    // public AudioClip shootClip;

    public abstract void Fire();
}
