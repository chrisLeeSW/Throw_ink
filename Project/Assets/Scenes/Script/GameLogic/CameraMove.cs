using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private float xDefaultRotation=5f;
    public float xRoation;


    public float yCameraPosition ;
    private float yRotation;
    private float cameraMoveFrontBackSpeed;

    public Transform playerTransform;
    public float distanceFromPlayer = 10f;
    public float xRotationDuration = 5f;
    //private float rotationSpeed = 100f;

    public float CameraMoveSpeed
    {
        get { return cameraMoveFrontBackSpeed; }
        set { cameraMoveFrontBackSpeed = value; }
    }
    public float YCameraPosition
    {
        set{ yCameraPosition = value; }
    }
    public float XRotation
    {
        get;  set;
    }
    private void Awake()
    {
    }
    private void FixedUpdate()
    {
        yRotation = playerTransform.eulerAngles.y;
        if (xRoation > xRotationDuration)
            xRoation = xRotationDuration;
        else if (xRoation < -xRotationDuration)
            xRoation = -xRotationDuration;

        transform.rotation = Quaternion.Euler(xDefaultRotation + xRoation, yRotation, 0);
    }
    public void SyncWithPlayer(Vector3 playerDirection)
    {
        Vector3 offset = -playerTransform.forward * distanceFromPlayer;
        Vector3 desiredPosition = playerTransform.position + offset;
        desiredPosition.y = yCameraPosition;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, cameraMoveFrontBackSpeed * Time.deltaTime);

    }
}

/*
 * 유기된ㅋ 코드들
 
    private void Update()
    {
        if (Input.GetKey(KeyCode.Keypad8))
        {
            xRotation -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Keypad5))
        {
            xRotation += rotationSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Keypad6))
        {
            yRotation += rotationSpeed * Time.deltaTime;
            yPlayerRotation += rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            yRotation -= rotationSpeed * Time.deltaTime;
            yPlayerRotation -= rotationSpeed * Time.deltaTime;
        }

    }


 */