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
