using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMent : MonoBehaviour
{
    public LayerMask layerMask;
    private Rigidbody rb;

    private Vector3 direction;
    private float defaultPlayerSpeed = 5f;
    private float moveSpeed = 5f;
    private float jumpForce =5f;
    private uint jumpState = 0;
    private uint maxJumpState = 2;
    private bool isWallCollide;
    private Animator ani;



    private float yRotation;
    private float rotationSpeed;


    private bool isOnHighSpeedPad = false;
    private float speedIncreaseRate = 1f;
    private float speedDecreaseRate = 0.5f;
    private float maxSpeed = 10f;
    public float playerGravity = 9.8f;


    public uint JumpState
    {
        set { jumpState = value; }
    }
    public float DefaultMoveSpeed
    {
        get { return defaultPlayerSpeed; }
    }
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
    public float YRotation
    {
        get { return yRotation; }
        set { yRotation = value; }
    }
    public float RotationSpeed
    {
        set { rotationSpeed = value; }
    }
    public Quaternion PlayerRoation
    {
        get { return rb.rotation; }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {

        var position = rb.position;
        position += direction * moveSpeed * Time.deltaTime;
        rb.MovePosition(position);

        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void PlayerMove()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && jumpState < maxJumpState)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpState++;
            ani.SetTrigger("Jumping");
        }

        direction = transform.TransformDirection(new Vector3(h, 0, v));
        var directionMag = direction.magnitude;
        if (directionMag > 1)
        {
            direction.Normalize();
        }
        ani.SetFloat("Speed", directionMag);
    }

    public Vector3 GetDirection()
    {
        return direction;
    }

    public float GetPlayerSpeed()
    {
        return moveSpeed;
    }

    public Vector3 GetPlayerPosition()
    {
        return rb.position;
    }

    public void RotatePlayer(float yRotation, float rotationSpeed)
    {
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }



    public void JumpCollisionByPad(float newJumpPad1Power, uint jumpCount)
    {
        rb.velocity = new Vector3(rb.velocity.x, newJumpPad1Power, rb.velocity.z);
        //rb.AddForce(Vector3.up * newJumpPad1Power, ForceMode.Impulse);
        jumpState = jumpCount;
        ani.SetTrigger("Jumping");
    }

    public void IsGroundAnimationSet()
    {
        jumpState = 0;
        ani.SetTrigger("Ground");
    }


    private void OnCollisionEnter(Collision collision)
    {
        string collisionTag = collision.collider.tag;
        switch (collisionTag)
        {
            case "Ground":
                moveSpeed = defaultPlayerSpeed;
                IsGroundAnimationSet();
                break;
            case "JumpPadV1":
                float newJumpPad1Power = 15f;
                JumpCollisionByPad(newJumpPad1Power, 1);
                break;
            case "JumpPadV2":
                float newJumpPad2Power = 20f;
                JumpCollisionByPad(newJumpPad2Power, 2);
                break;
            case "UniqueJumpPad":
                float forwardForce = 15f;
                float upwardForce = 0.5f;
                float duration = 0.5f;
                StartCoroutine(UniqueJumpRoutine(duration, forwardForce, upwardForce));
                break;
            case "LowSpeedPad":
                moveSpeed = 2f;
                IsGroundAnimationSet();
                break;
            default:
                IsGroundAnimationSet();
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
        float upwardVelocity = 2 * peakHeight / timeToPeak;
        float gravity = -2 * peakHeight / Mathf.Pow(timeToPeak, 2);

        rb.velocity = new Vector3(forwardForce, upwardVelocity, 0);
        float elapsedTime = 0;

        while (elapsedTime < timeToPeak)
        {
            rb.velocity = new Vector3(forwardForce, rb.velocity.y + gravity * Time.deltaTime, 0);
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
