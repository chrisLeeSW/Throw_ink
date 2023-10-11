using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootcontroller : MonoBehaviour
{
    public GameObject testPivoit;
    private float xRotation;
    private float yRotation;
    private float rotationSpeed = 100f;
    private void FixedUpdate()
    {
        if (xRotation > 30f)
            xRotation = 30f;
        else if (xRotation < -35f)
            xRotation = -35f;
        testPivoit.gameObject.transform.rotation = Quaternion.Euler(xRotation , yRotation, 0);
    }
    public void AimSetting()
    {
        if(Input.GetKey(KeyCode.Keypad6))
        {
            yRotation += rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            yRotation -= rotationSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Keypad8))
        {
            xRotation -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Keypad5))
        {
            xRotation += rotationSpeed * Time.deltaTime;
        }
    }
}
