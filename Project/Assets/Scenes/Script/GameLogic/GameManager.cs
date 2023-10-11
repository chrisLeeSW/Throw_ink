using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerManager playerManager;
    public CameraMove caremaMove;
    public float cameraoffset =1.5f;

    public List<GameObject> stages;
    private int currentStages;

    private float yRotation;
    private float rotationSpeed = 100f;

    private Vector3 prevMousePosition;
    private Vector3 endMousePosition;
    
    private void Start()
    {
        caremaMove.CameraMoveSpeed = playerManager.GetPlayerMoveSpeed();
    }
    private void FixedUpdate()
    {
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(1)) 
        {
            Debug.Log($"시작 마우스 :{Input.mousePosition}");
            prevMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1))
        {
            prevMousePosition = Vector3.zero;
            endMousePosition = Vector3.zero;
        }
        if (Input.GetMouseButton(1))
        {
            endMousePosition = Input.mousePosition;
            //yRotation += Input.mousePosition.x * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Keypad6))
        {
            yRotation += rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            yRotation -= rotationSpeed * Time.deltaTime;
        }
        
        var newDirceiton = endMousePosition - prevMousePosition;
        newDirceiton.Normalize();
        playerManager.GetPlayerMoveMent().YRotation = newDirceiton.x;
        playerManager.GetPlayerMoveMent().RotationSpeed = rotationSpeed;
        playerManager.GetPlayerShootController().YRotation = newDirceiton.x;
        playerManager.GetPlayerShootController().RotationSpeed = rotationSpeed;
        playerManager.GetPlayerShootController().XRotation = -newDirceiton.y;
        caremaMove.xRoation = -newDirceiton.y;

        var direction = playerManager.GetPlayerDirection();
        caremaMove.SyncWithPlayer(direction);
        caremaMove.YCameraPosition = playerManager.GetPlayerPosition().y + cameraoffset;
    }
}
