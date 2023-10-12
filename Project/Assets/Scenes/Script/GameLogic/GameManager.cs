using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

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

    [Header("플레이어 오브젝트 및 플레이어 매니저 관리")]
    public GameObject playerCharacter;
    public PlayerManager playerManager;
    [Header("카메라")]
    public CameraMove caremaMove;
    public float cameraoffset =1.5f;
    [Header("스테이지")]
    public List<GameObject> stages;
    private int currentStages;


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
        }
        
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
}
