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
        if (xRotation > 60f)
            xRotation = 60f;
        else if (xRotation < -60f)
            xRotation = -60f;
        if (yRotation > 60f)
            yRotation = 60f;
        else if (yRotation < -60f)
            yRotation = -60f;
        testPivoit.gameObject.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AimHandle()
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
}
