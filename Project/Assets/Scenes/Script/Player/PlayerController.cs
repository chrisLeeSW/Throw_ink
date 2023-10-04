using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private CapsuleCollider playerCollider; // player ���� ����� �簢������ �浹 ó���� �۾��� ���� ���� ����
    private Vector3 direction;
    private float speed = 5f;

    public GameObject testPivoit;
    public float xRotation;
    public float yRotation;
    private float rotationSpeed = 100f;
    // ���̽�ƽ���� ���� �ؾߵ� �� �� ��� ��ġ ���� �����Ŵ
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        // ���� ������ ���´ٸ� �۾��� ���� ��������� ����
    }

    private void FixedUpdate()
    {
        Vector3 position = rb.position;
        position += direction * speed * Time.deltaTime;
        rb.MovePosition(position);

        if (xRotation > 60f)
            xRotation = 60f;
        else if (xRotation < -60f)
            xRotation = -60f;
        if (yRotation > 60f)
            yRotation = 60f;
        else if (yRotation < -60f)
            yRotation = -60f;

        testPivoit.gameObject.transform.rotation= Quaternion.Euler(xRotation, yRotation, 0);
    }
    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        direction = new Vector3(h, 0, v);
        var directionMag = direction.magnitude;
        if (directionMag > 1)
        {
            direction.Normalize();
        }
        if(Input.GetKey(KeyCode.Keypad8))
        {
            xRotation -= rotationSpeed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.Keypad5))
        {
            xRotation += rotationSpeed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.Keypad6))
        {
            yRotation += rotationSpeed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.Keypad4))
        {
            yRotation -= rotationSpeed * Time.deltaTime;
        }

    }
}
