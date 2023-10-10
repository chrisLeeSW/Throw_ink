using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerManager playerManager;
    public CameraMove caremaMove;
    public float cameraoffset =1.5f;

    private void Start()
    {
        caremaMove.CameraMoveSpeed = playerManager.GetPlayerMoveSpeed();
    }
    private void Update()
    {
        var direction = playerManager.GetPlayerDirection();
        caremaMove.SyncWithPlayer(direction);
        caremaMove.YCameraPosition = playerManager.GetPlayerPosition().y + cameraoffset;
        
    }
}
