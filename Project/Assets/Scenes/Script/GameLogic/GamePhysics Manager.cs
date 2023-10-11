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
                // 포물선으로 빠르게 앞으로 나가게끔해야함
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
