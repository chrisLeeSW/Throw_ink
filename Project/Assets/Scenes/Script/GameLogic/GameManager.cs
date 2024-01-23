using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public VirtualJoystick moveJoystick;
    public VirtualJoystick shootControllerStick;

    public Button shootingButton;
    public Button playerJumping;

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
    public float XRoationSpeed
    {
        get { return xRtoationSspeed; }
        set { xRtoationSspeed = value;}
    }
    public float RotationSpeed
    {
        get { return rotationSpeed; }
        set { rotationSpeed = value; }
    }
    public bool IsPause
    {
        get; set;
    }
    public bool IsShooting
    {
        get;set;
    }
    public bool PlayerJumping
    {
        get;set;
    }
    private void Awake()
    {
        
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "chapter 0-0 tutorial")
        {
            playerManager.GetPlayerMoveMent().YRotation += 180f;
            playerManager.GetPlayerShootController().YRotation += 180f;
        }

        rotationSpeed = OnGameData.instance.Sensitivity;
        cameraaMove.CameraMoveSpeed = playerManager.GetPlayerMoveSpeed();
    }
    private void FixedUpdate()
    {

#if UNITY_STANDALONE
        var newDirceiton = endMousePosition - prevMousePosition;
        newDirceiton.Normalize();

        var xRot = newDirceiton.x * xRtoationSspeed;
        playerManager.GetPlayerMoveMent().YRotation += xRot * rotationSpeed;
        playerManager.GetPlayerMoveMent().RotationSpeed += rotationSpeed;

        playerManager.GetPlayerShootController().YRotation += xRot * rotationSpeed  ;
        playerManager.GetPlayerShootController().RotationSpeed += rotationSpeed ;
        playerManager.GetPlayerShootController().XRotation += -newDirceiton.y;

        cameraaMove.xRoation += -newDirceiton.y /6;
        shootingButton.gameObject.SetActive(false);
        playerJumping.gameObject.SetActive(false);
        moveJoystick.gameObject.SetActive(false);
        shootControllerStick.gameObject.SetActive(false);   
#elif UNITY_ANDROID || UNITY_IOS
        playerManager.GetPlayerMoveMent().YRotation += shootControllerStick.GetAxis(VirtualJoystick.Axis.Horizontal) * rotationSpeed;
        playerManager.GetPlayerMoveMent().RotationSpeed += rotationSpeed;

        playerManager.GetPlayerShootController().YRotation += shootControllerStick.GetAxis(VirtualJoystick.Axis.Horizontal) * rotationSpeed;
        playerManager.GetPlayerShootController().RotationSpeed += rotationSpeed;
        playerManager.GetPlayerShootController().XRotation += -shootControllerStick.GetAxis(VirtualJoystick.Axis.Vertical);
        cameraaMove.xRoation += -shootControllerStick.GetAxis(VirtualJoystick.Axis.Vertical);
#endif


        var direction = playerManager.GetPlayerDirection();
        cameraaMove.YCameraPosition = playerManager.GetPlayerPosition().y + cameraoffset;
        cameraaMove.SyncWithPlayer(direction);
    }
    private void Update()
    {

        if (!IsClear && !IsPlayerDie && !IsGameOver &&!IsPause)
        {
#if UNITY_ANDROID || UNITY_IOS
            

            playerManager.SetPlayerDirection(moveJoystick.GetAxis(VirtualJoystick.Axis.Horizontal), moveJoystick.GetAxis(VirtualJoystick.Axis.Vertical));
#elif UNITY_STANDALONE
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
            float distance = cameraaMove.DistanceFromPlayer;
            distance -= mouseWheelInput;
            if(distance >5 && distance <15)
                cameraaMove.DistanceFromPlayer =distance;

            playerManager.GetPlayerMoveMent().PlayerMove();
#endif
        }
        else if(!IsPause)
        {
            if (IsPlayerDie)
            {
                OnGameData.instance.ResultStagePlay = 0;

            }
            else
            {
                GameResult();
            }
            SceneManager.LoadScene("Result-V1.0");
 
        }
       
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

    public void SetButton()
    {
        IsShooting = !IsShooting;
        playerManager.GetPlayerShootPainter().SetBoolIsShooting(IsShooting);
    }
    public void SetButtonJump()
    {
        playerManager.GetPlayerMoveMent().PlayerJump();
    }
}
