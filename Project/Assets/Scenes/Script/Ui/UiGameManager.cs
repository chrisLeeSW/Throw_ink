using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class UiGameManager : MonoBehaviour
{
    public GameObject MainLooby;
    public GameObject joystick;

    private void Awake()
    {
        joystick.SetActive(false);
    }
}
