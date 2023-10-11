using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePhysicsManager : MonoBehaviour
{
    public PlayerMoveMent playerMoveMent;
    private void OnCollisionEnter(Collision collision)
    {
        string collisionTag = collision.collider.tag;
        switch (collisionTag)
        {
            case "Ground":
                playerMoveMent.JumpState = 0;
                playerMoveMent.MoveSpeed = playerMoveMent.DefaultMoveSpeed;
                playerMoveMent.IsGroundAnimationSet();
                break;
            case "JumpPadV1":
                float newJumpPad1Power = 10f;
                playerMoveMent.JumpCollisionByPad(newJumpPad1Power, 1);
                break;
            case "JumpPadV2":
                float newJumpPad2Power = 15f;
                playerMoveMent.JumpCollisionByPad(newJumpPad2Power, 2);
                break;
            case "UniqueJumpPad":
                //float forwardForce = 10f;  
                //float upwardForce = 0.5f;    
                //float duration = 0.5f;      

                //StartCoroutine(UniqueJumpRoutine(duration, forwardForce, upwardForce));
                // ���������� ������ ������ �����Բ��ؾ���
                break;
            //case "LowSpeedPad":
            //    moveSpeed = 2f;
            //    break;
            default:
                playerMoveMent.IsGroundAnimationSet();
                break;
        }
    }
}
