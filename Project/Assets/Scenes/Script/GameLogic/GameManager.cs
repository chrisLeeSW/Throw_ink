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

    private float rotationSpeed = 1f;//

    private Vector3 prevMousePosition;
    private Vector3 endMousePosition;

   
    private float xRtoationSspeed = 1f;

    public bool IsGameOver
    {
        get; set;
    }
    public bool IsPlayerDie
    {
        get; set;
    }
    public bool IsClear
    {
        get; set;
    }

    private void Start()
    {
        rotationSpeed = OnGameData.instance.Sensitivity;
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
    }
    private void Update()
    {

        if (!IsClear && !IsPlayerDie && !IsGameOver)
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
            if (IsPlayerDie)
                OnGameData.instance.ResultStagePlay = 0;
            else
                GameResult();
            SceneManager.LoadScene("Result-V1.0");
        }
        playerManager.GetPlayerMoveMent().PlayerMove();
    }

    private void GameResult()
    {
        var result = UiGameManager.instance.GetGameResult();
        if (result < UiGameManager.instance.IncreasBar)
            OnGameData.instance.ResultStagePlay = 0;
        else if (result > UiGameManager.instance.IncreasBar && result < UiGameManager.instance.IncreasBar * 2)
            OnGameData.instance.ResultStagePlay = 1;
        else if (result > UiGameManager.instance.IncreasBar * 2 && result < UiGameManager.instance.IncreasBar * 3)
            OnGameData.instance.ResultStagePlay = 2;
        else if (result > UiGameManager.instance.IncreasBar * 3)
            OnGameData.instance.ResultStagePlay = 3;

    }
}
