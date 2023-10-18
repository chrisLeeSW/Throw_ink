using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerShootPainter : MonoBehaviour
{
    protected enum GunType
    {
        NormalGun,
    } 

    public ParticleSystem particle;
    public Transform gunPivot;
    //private GunType gunType =GunType.NormalGun;
    private bool isShooting;

    private AudioSource playerAudio;
    public AudioMixer audioMixer;
    public AudioClip playerShoot;

    public bool IsPause { get; set; }
    private void Awake()
    {
        playerAudio = GetComponent<AudioSource>();
        //if (OnGameData.instance.SoundVolum == -40f) audioMixer.SetFloat("PlayerShoot", -80f);
        //else audioMixer.SetFloat("PlayerShoot", OnGameData.instance.SoundVolum);
    }
    private void FixedUpdate()
    {

    }
    private void Update()
    {
        if (!IsPause)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isShooting = !isShooting;
            }
            if (isShooting)
            {
                particle.Play();
                if (!playerAudio.isPlaying)
                    playerAudio.PlayOneShot(playerShoot);
            }
            else if (!isShooting)
            {
                particle.Stop();
                playerAudio.Stop();
            }
        }
        else
        {
            particle.Stop();
            playerAudio.Stop();
        }
    }

    public bool GetIsShooting()
    {
        return isShooting;  
    }    
}
