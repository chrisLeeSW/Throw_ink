using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        playerMove.PlayerMove();
        playerShootController.AimSetting();
    }

    public Vector3 GetPlayerDirection()
    {
        return playerMove.GetDirection();
    }

    public float GetPlayerMoveSpeed()
    {
        return playerMove.GetPlayerSpeed();
    }


    public void RotatePlayer(float yRotation,float rotationSpeed)
    {
        playerMove.RotatePlayer(yRotation, rotationSpeed);
    }
}
