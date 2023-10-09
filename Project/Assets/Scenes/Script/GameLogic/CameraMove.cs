using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    
    private float xRotation;
    private float yRotation;
    private float rotationSpeed = 100f;
    private float cameraMoveFrontBackSpeed;
    private float cameraMoveLeftRightSpeed = 100f;
    public float CurrentYRotation
    {
        get { return yRotation; }
    }
    public float CameraMoveLRSpeed
    {
        get { return cameraMoveLeftRightSpeed; }
    }
    public float CameraMoveSpeed
    {
        get { return cameraMoveFrontBackSpeed; }
        set { cameraMoveFrontBackSpeed = value; }
    }
    private void FixedUpdate()
    {
        if (xRotation > 15f)
            xRotation = 15f;
        else if (xRotation < -15f)
            xRotation = -15f;
        Camera.main.gameObject.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

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
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            yRotation -= rotationSpeed * Time.deltaTime;
        }

    }
    public void SyncWithPlayer(Vector3 playerDirection)
    {
        transform.position += playerDirection * cameraMoveFrontBackSpeed * Time.deltaTime;
    }
}
