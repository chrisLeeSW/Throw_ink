using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if (gameManagerSingleTon == null)
            {
                gameManagerSingleTon = FindObjectOfType<GameManager>();
            }
            return gameManagerSingleTon;
        }
    }
    private static GameManager gameManagerSingleTon;


    public PlayerManager playerManager;
    [Header("Ä«¸Þ¶ó")]
    public CameraMove caremaMove;
    public float cameraoffset =1.5f;


    private float rotationSpeed = 100f;

    private Vector3 prevMousePosition;
    private Vector3 endMousePosition;
    
    private void Start()
    {
        caremaMove.CameraMoveSpeed = playerManager.GetPlayerMoveSpeed();
    }
    private void FixedUpdate()
    {
        var newDirceiton = endMousePosition - prevMousePosition;
        newDirceiton.Normalize();
        playerManager.GetPlayerMoveMent().YRotation += newDirceiton.x;
        playerManager.GetPlayerMoveMent().RotationSpeed = rotationSpeed;

        playerManager.GetPlayerShootController().YRotation += newDirceiton.x;
        playerManager.GetPlayerShootController().RotationSpeed = rotationSpeed;
        playerManager.GetPlayerShootController().XRotation += -newDirceiton.y;

        caremaMove.xRoation += -newDirceiton.y;

        var direction = playerManager.GetPlayerDirection();
        caremaMove.YCameraPosition = playerManager.GetPlayerPosition().y + cameraoffset;
        caremaMove.SyncWithPlayer(direction);

        playerManager.GetPlayerMoveMent().PlayerMove();
    }
    private void Update()
    {

        if (!UiGameManager.instance.isClear && !UiGameManager.instance.isGameover)
        {
            if (Input.GetMouseButtonDown(1))
            {
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
            }

            
        }
        else 
        {
            SceneManager.LoadScene("ResultScene");
        }
    }
}
