using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 direction;
    private float moveSpeed = 5f;

    public GameObject testPivoit;
    public float xRotation;
    public float yRotation;
    private float rotationSpeed = 100f;

    private float jumpForce = 5f;
    private uint jumpState = 0;
    private uint maxJumpState = 2;

    private bool isUniqueJump;
    private float uniqueJumpForce;
    private float uniqueJumpAngle;
    // ���̽�ƽ���� ���� �ؾߵ� �� �� ��� ��ġ ���� �����Ŵ

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // ���� ������ ���´ٸ� �۾��� ���� ��������� ����
    }

    private void FixedUpdate()
    {
        Vector3 position = rb.position;
        position += direction * moveSpeed * Time.deltaTime;
        //rb.MovePosition(position);
        rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);

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
    void Update()
    {
        if(isUniqueJump)
        {
            // ����� ������ �߻��ϴ� ���� �����ϸ� �Է°��� ���� �ޱ�
        }

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && jumpState<maxJumpState)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpState++;
        }

        direction = new Vector3(h, 0, v);
        var directionMag = direction.magnitude;
        if (directionMag > 1)
        {
            direction.Normalize();
        }

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

    private void OnCollisionEnter(Collision collision)
    {
        string collisionTag = collision.collider.tag;
        switch(collisionTag)
        {
            case "Ground":
                jumpState = 0;
                isUniqueJump = false;
                break;
            case "JumpPadV1":
                float newJumpPad1Power = 10f;
                rb.AddForce(Vector3.up * newJumpPad1Power, ForceMode.Impulse);
                jumpState = 1;
                break;
            case "JumpPadV2":
                float newJumpPad2Power = 15f;
                rb.AddForce(Vector3.up * newJumpPad2Power, ForceMode.Impulse);
                jumpState = 2;
                break;
           
        }
        //if (collision.collider.CompareTag("Ground"))
        //{
        //    jumpState = 0;
        //}

    }

    private void OnTriggerStay(Collider other)
    {
        string collisionTag = other.tag;
        switch (collisionTag)
        {
            case "UniqueJumpPad":
                GameObject collidedObject = other.gameObject;

                if (collidedObject.transform.parent != null && collidedObject.transform.parent.name == "UniqueJumpPad")
                {
                    switch (collidedObject.name)
                    {
                        case "JumpPad1":
                            Debug.Log("Collision with JumpPad1");
                            isUniqueJump = true;
                            float launchForce = 100f; // �÷��̾ ���ư� ��
                            float jumpForce = 3f;
                            Vector3 launchDirection = transform.forward * launchForce; // �հ� �� ���� ��η�
                            launchDirection += Vector3.up * jumpForce;
                            rb.AddForce(launchDirection , ForceMode.Impulse);
                            break;

                        case "JumpPad2":
                            Debug.Log("Collision with JumpPad2");
                            // �ʿ��� �߰� ó�� �۾�
                            break;
                    }
                }
                break;
        }
    }
    
}
