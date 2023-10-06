using System.Collections;
using System.Collections.Generic;
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
    // 조이스틱으로 변경 해야됨 총 알 쏘는 위치 값을 변경시킴

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // 추후 스탯이 나온다면 작업을 파일 입출력으로 변경
    }

    private void FixedUpdate()
    {
        Vector3 position = rb.position;
        position += direction * moveSpeed * Time.deltaTime;
        rb.MovePosition(position);


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
        if (collision.collider.CompareTag("Ground"))
        {
            jumpState = 0;
        }
    }
}
