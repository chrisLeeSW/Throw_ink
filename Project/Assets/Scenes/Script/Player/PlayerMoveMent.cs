using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private float punchTrapPlayingTime = 0.5f;
    private float punchTrapForce = 200f;

    public Transform playerSpwanPosition;

    private bool isOnFlatGround = false;
    private float raycastDistance = 10f;
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
    public bool IsPasue
    { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        rb.position = playerSpwanPosition.position;

       
    }
    private void FixedUpdate()
    {

        //var position = rb.position;
        //position += direction * moveSpeed * Time.deltaTime;
        //rb.MovePosition(position);

        rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);

        transform.rotation = Quaternion.Euler(0, yRotation , 0);
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
        if (!IsPasue)
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");
            
            if (jumpState < maxJumpState && (Input.GetKeyDown(KeyCode.Space)) && canJump)
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


        ani.SetBool("Jumping", true);
        ani.SetBool("isGround", false);
    }

    public void IsGroundCollisionSet()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            Vector3 playerToHit = hit.point - transform.position;
            float angle = Vector3.Angle(Vector3.up, hit.normal);

            if (angle < 30f && playerToHit.y < 0.1f) // 예시로 각도가 30도 미만이고 Y 값 차이가 0.1보다 작은 경우를 평평한 바닥으로 판단합니다.
            {
                isOnFlatGround = true;
            }
            else
            {
                isOnFlatGround = false;
            }
        }
        else
        {
            isOnFlatGround = false;
        }
        if (isOnFlatGround)
        {
            jumpState = 0;
            ani.SetBool("isGround", true);
            ani.SetBool("Jumping", false);
            isPlayerJumping = false;
            canJump = true;
        }

        jumpState = 0;
        ani.SetBool("isGround", true);
        ani.SetBool("Jumping", false);
        isPlayerJumping = false;
        canJump = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        string collisionTag = collision.collider.tag;
        switch (collisionTag)
        {
            case "Ground":
                moveSpeed = defaultPlayerSpeed;
                IsGroundCollisionSet();

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
                moveSpeed = 1.5f;
                jumpState = 0;
                ani.SetBool("isGround", true);
                ani.SetBool("Jumping", false);
                isPlayerJumping = false;
                canJump = true;
                IsGroundCollisionSet();
                break;
            case "PunchTrap":
                StartCoroutine(PunchForceRoutine(punchTrapPlayingTime, punchTrapForce));
                break;
            case "JumpPadV3":
                float newJumpPad3Power = 15f;
                rb.AddForce(Vector3.up * newJumpPad3Power);
                //JumpCollisionByPad(newJumpPad3Power,0);
                break;
            default:
                IsGroundCollisionSet();
                break;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        string collisionTag = collision.collider.tag;
        switch (collisionTag)
        {
            case "LowSpeedPad":
                moveSpeed = defaultPlayerSpeed;
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
        else if (other.CompareTag("Failing"))
        {
            UiGameManager.instance.PlayerLifeDecrease();
            transform.position = playerSpwanPosition.position;
        }
        else if (other.CompareTag("Tutorial"))
        {

            transform.position = playerSpwanPosition.position;
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
