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
        if (xRotation > 45f)
            xRotation = 45f;
        else if (xRotation < -45f)
            xRotation = -45f;
        testPivoit.gameObject.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
    public void AimSetting()
    {
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
