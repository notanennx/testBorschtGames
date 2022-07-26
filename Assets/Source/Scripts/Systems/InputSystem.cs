using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class InputSystem : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private FloatingJoystick mainJoystick;

    // Actions
    public static Action<Vector2> OnInputRecieved;

    // Update
    private void Update()
    {
        BroadcastInput();
    }

    // Broadcasts joystick input
    private void BroadcastInput()
    {
        float vInput = mainJoystick.Vertical;
        float hInput = mainJoystick.Horizontal;
        if ((vInput != 0) || (hInput != 0))
            OnInputRecieved?.Invoke(new Vector2(hInput, vInput));
    }
}
