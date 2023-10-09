using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMent : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 direction;
    private float moveSpeed = 5f;
    private float jumpForce = 5f;
    private uint jumpState = 0;
    private uint maxJumpState = 2;
    private Animator ani;


    private float yRotation;
    private float rotationSpeed = 100f;


    public Transform playerHead;
    public Transform playerBody;
    //public Transform playerRoot;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Vector3 position = rb.position;
        position += direction * moveSpeed * Time.deltaTime;
        rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);

        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        

        playerHead.rotation = Quaternion.Euler(0, yRotation, 0);
        playerBody.rotation = Quaternion.Euler(0, yRotation, 0);
        //playerRoot.rotation = Quaternion.Euler(0, yRotation, 0);

    }

    public void PlayerMove()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && jumpState < maxJumpState)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpState++;
            ani.SetTrigger("Jumping");
        }

        direction = new Vector3(h, 0, v);
        var directionMag = direction.magnitude;
        if (directionMag > 1)
        {
            direction.Normalize();
        }

        if (Input.GetKey(KeyCode.Keypad6))
        {
            yRotation += rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            yRotation -= rotationSpeed * Time.deltaTime;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground") && jumpState > 0)
        {
            ani.SetTrigger("Ground");

            jumpState = 0;
        }
    }
    
    
}
