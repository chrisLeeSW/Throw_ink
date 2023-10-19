using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private PlayerMoveMent playerMove;
    private PlayerShootcontroller playerShootController;
    private PlayerShootPainter playerShootPainter;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMoveMent>();
        playerShootController= GetComponent<PlayerShootcontroller>();
        playerShootPainter = GetComponent<PlayerShootPainter>();
    }

    private void Update()
    {
        
    }
    
    public Vector3 GetPlayerDirection()
    {
        return playerMove.GetDirection();
    }

    public float GetPlayerMoveSpeed()
    {
        return playerMove.GetPlayerSpeed();
    }

    public Vector3 GetPlayerPosition()
    {
        return playerMove.GetPlayerPosition();
    }

    public void RotatePlayer(float yRotation,float rotationSpeed)
    {
        playerMove.RotatePlayer(yRotation, rotationSpeed);
    }

    public PlayerMoveMent GetPlayerMoveMent()
    {
        //playerMove.PlayerMove();
        return playerMove;
    }

    public PlayerShootcontroller GetPlayerShootController()
    {
        return playerShootController;
    }


    public PlayerShootPainter GetPlayerShootPainter()
    {
        return playerShootPainter;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Failing"))
        {
            playerShootPainter.PlayerIsShootingReset();
        }
    }
}
