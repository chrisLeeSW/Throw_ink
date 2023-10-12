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

    [Header("�÷��̾� ������Ʈ �� �÷��̾� �Ŵ��� ����")]
    public GameObject playerCharacter;
    public PlayerManager playerManager;
    [Header("ī�޶�")]
    public CameraMove caremaMove;
    public float cameraoffset =1.5f;
    [Header("��������")]
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
            Debug.Log($"���� ���콺 :{Input.mousePosition}");
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
