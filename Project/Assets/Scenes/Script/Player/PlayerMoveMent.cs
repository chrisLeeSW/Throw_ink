using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMent : MonoBehaviour
{
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


    private bool canJump=true;
    private float jumpDelay=0.3f;
    private bool isPlayerJumping;

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
        get { return rotationSpeed; }
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

        //var position = rb.position;
        //position += direction * moveSpeed * Time.deltaTime;
        //rb.MovePosition(position);

        rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);

        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        if(isPlayerJumping)
        {

            isPlayerJumping = false;
            jumpState++;
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            ani.SetBool("Jumping", true);
            ani.SetBool("isGround", false);
            canJump = false;
            StartCoroutine(JumpDelayRoutine());
        }
    }
    public void PlayerMove()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        if ( jumpState < maxJumpState && (Input.GetKeyDown(KeyCode.Space))&& canJump)
        {
            isPlayerJumping = true;
        }
        
        direction = transform.TransformDirection(new Vector3(h, 0, v));
        var directionMag = direction.magnitude;
        if (directionMag > 1)
        {
            direction.Normalize();
        }
        ani.SetFloat("Speed", directionMag);
    }
    private IEnumerator JumpDelayRoutine()
    {
        yield return new WaitForSeconds(jumpDelay); 
        canJump = true; 
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

        Debug.Log("Hello");
        ani.SetBool("Jumping", true);
        ani.SetBool("isGround", false);
    }

    public void IsGroundAnimationSet()
    {
        jumpState = 0;
        ani.SetBool("isGround",true);
        ani.SetBool("Jumping", false);
        isPlayerJumping = false;
    }

    public float punchTrapPlayingTime = 0.5f;
    public float punchTrapForce = 200f;

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
                float newJumpPad1Power = 10f;
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
            case "PunchTrap":
                Debug.Log("���ƾƾ�");
                StartCoroutine(PunchForceRoutine(punchTrapPlayingTime, punchTrapForce));
                break;
            default:
                IsGroundAnimationSet();
                break;
        }
    }
    private IEnumerator PunchForceRoutine(float duration, float maxForce)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float force = Mathf.Lerp(0, maxForce, t);
            rb.AddForce(Vector3.right * force);
            elapsedTime += Time.deltaTime;
            yield return null;
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
