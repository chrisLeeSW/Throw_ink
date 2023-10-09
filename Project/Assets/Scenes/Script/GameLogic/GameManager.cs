using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerManager playerManager;
    public CameraMove caremaMove;


    private void Start()
    {
        caremaMove.CameraMoveSpeed = playerManager.GetPlayerMoveSpeed();
    }
    private void Update()
    {
        var direction = playerManager.GetPlayerDirection();
        caremaMove.SyncWithPlayer(direction);

        playerManager.RotatePlayer(caremaMove.CurrentYRotation,caremaMove.CameraMoveLRSpeed);
    }
}
