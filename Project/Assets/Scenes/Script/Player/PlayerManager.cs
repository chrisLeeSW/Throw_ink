using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerMoveMent playerMove;
    private PlayerShootcontroller playerAim;

    private void Awake()
    {
        playerMove =GetComponent<PlayerMoveMent>();
        playerAim = GetComponent<PlayerShootcontroller>();
    }

    private void Update()
    {
        playerMove.PlayerMoveHandle();
        playerAim.AimHandle();
    }
}
