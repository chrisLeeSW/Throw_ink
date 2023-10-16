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
    public CameraMove cameraaMove;
    public float cameraoffset =1.5f;

    [SerializeField,Range(1,5)]
    private float rotationSpeed = 5f;

    private Vector3 prevMousePosition;
    private Vector3 endMousePosition;

    [SerializeField, Range(0.1f, 100f)]
    private float xRtoationSspeed = 1f;

    private void Start()
    {
        cameraaMove.CameraMoveSpeed = playerManager.GetPlayerMoveSpeed();
    }
    private void FixedUpdate()
    {
        var newDirceiton = endMousePosition - prevMousePosition;
        newDirceiton.Normalize();

        var xRot = newDirceiton.x * xRtoationSspeed;
        playerManager.GetPlayerMoveMent().YRotation += xRot * rotationSpeed;
        playerManager.GetPlayerMoveMent().RotationSpeed += rotationSpeed;

        playerManager.GetPlayerShootController().YRotation += xRot * rotationSpeed  ;
        playerManager.GetPlayerShootController().RotationSpeed += rotationSpeed ;
        playerManager.GetPlayerShootController().XRotation += -newDirceiton.y;

        cameraaMove.xRoation += -newDirceiton.y /6;

        var direction = playerManager.GetPlayerDirection();
        cameraaMove.YCameraPosition = playerManager.GetPlayerPosition().y + cameraoffset;
        cameraaMove.SyncWithPlayer(direction);

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
            float mouseWheelInput = Input.GetAxis("Mouse ScrollWheel");
            cameraaMove.DistanceFromPlayer -=mouseWheelInput;
            
        }
        else 
        {
            SceneManager.LoadScene("ResultScene");
        }
    }
}
