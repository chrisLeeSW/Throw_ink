using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickTest : MonoBehaviour
{
    public VirtualJoystick2Test joystick;

    private void Update()
    {
        Debug.Log($"{joystick.Value}");
    }
}
