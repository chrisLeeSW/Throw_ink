using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public float xRoation;


    public float yCameraPosition ;
    private float yRotation;
    private float cameraMoveFrontBackSpeed;

    public Transform playerTransform;
    private float distanceFromPlayer = 5f;
    private float rotationSpeed = 100f;

    public float CameraMoveSpeed
    {
        get { return cameraMoveFrontBackSpeed; }
        set { cameraMoveFrontBackSpeed = value; }
    }
    public float YCameraPosition
    {
        set { yCameraPosition = value; }
    }

    private void Awake()
    {
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Keypad8))
        {
            xRoation -= rotationSpeed * Time.deltaTime;
            if (xRoation < -20f)
                xRoation = -20f;
        }
        if (Input.GetKey(KeyCode.Keypad5))
        {
            xRoation += rotationSpeed * Time.deltaTime;
            if (xRoation > 20f)
                xRoation = 20f;
        }
    }
    private void FixedUpdate()
    {
        yRotation = playerTransform.eulerAngles.y;
    }
    public void SyncWithPlayer(Vector3 playerDirection)
    {
        Vector3 offset = -playerTransform.forward * distanceFromPlayer;
        Vector3 desiredPosition = playerTransform.position + offset;
        desiredPosition.y = yCameraPosition;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, cameraMoveFrontBackSpeed * Time.deltaTime);


        transform.rotation = Quaternion.Euler(xRoation, yRotation, 0);

        //transform.LookAt(playerTransform);
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