using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 direction;
    private float moveSpeed = 5f;
    private float defaultPlayerSpeed = 5f;


    private float xRotation;
    private float yRotation;
    private float rotationSpeed = 100f;

    private float jumpForce = 5f;
    private uint jumpState = 0;
    private uint maxJumpState = 2;

    // ���̽�ƽ���� ���� �ؾߵ� �� �� ��� ��ġ ���� �����Ŵ
    private bool isOnHighSpeedPad = false;
    private float speedIncreaseRate = 1f; // �ʴ� �ӵ� ������
    private float speedDecreaseRate = 0.5f; // �ʴ� �ӵ� ���ҷ�
    private float maxSpeed = 10f; // HightSpeedPad������ �ִ� �ӵ�

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
        
    }
    void Update()
    {


        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && jumpState < maxJumpState)
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
        switch (collisionTag)
        {
            case "Ground":
                jumpState = 0;
                moveSpeed = defaultPlayerSpeed;
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
            case "UniqueJumpPad":
                float forwardForce = 10f;   // ������ ���ư� ���� ũ��
                float upwardForce = 0.5f;     // �ʱ� ���� ���� ũ��
                float duration = 0.5f;      // ������ �������� ���� �ð�

                StartCoroutine(UniqueJumpRoutine(duration, forwardForce, upwardForce));
                // ���������� ������ ������ �����Բ��ؾ���
                break;
            case "LowSpeedPad":
                moveSpeed = 2f;
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HighSpeedPad"))
        {
            isOnHighSpeedPad = true;
            StartCoroutine(IncreaseSpeedRoutine());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HighSpeedPad"))
        {
            isOnHighSpeedPad = false;
            StartCoroutine(DecreaseSpeedRoutine());
        }
    }
    private IEnumerator UniqueJumpRoutine(float forwardForce, float peakHeight, float timeToPeak)
    {
        float upwardVelocity = 2 * peakHeight / timeToPeak; // �������� ���� �ӵ� ���
        float gravity = -2 * peakHeight / Mathf.Pow(timeToPeak, 2); // �������� ���� �߷� ���

        rb.velocity = new Vector3(forwardForce, upwardVelocity, 0); // �ʱ� �ӵ� ����
        float elapsedTime = 0;

        while (elapsedTime < timeToPeak)
        {
            rb.velocity = new Vector3(forwardForce, rb.velocity.y + gravity * Time.deltaTime, 0); // �߷¿� ���� �ӵ� ����
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator IncreaseSpeedRoutine()
    {
        while (isOnHighSpeedPad && moveSpeed < maxSpeed)
        {
            moveSpeed += speedIncreaseRate * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator DecreaseSpeedRoutine()
    {
        while (!isOnHighSpeedPad && moveSpeed > defaultPlayerSpeed)
        {
            moveSpeed -= speedDecreaseRate * Time.deltaTime;
            yield return null;
        }
        moveSpeed = defaultPlayerSpeed; 
    }
   
}