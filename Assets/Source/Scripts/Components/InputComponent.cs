using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class InputComponent : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private MoveComponent targetMove;

    // Refs
    private MoveComponent moveComponent;

    // Awaking
    private void Awake() => moveComponent = GetComponent<MoveComponent>();

    // Subscribe
    private void OnEnable() => InputSystem.OnInputReceived += TransmitInput;
    private void OnDisable() => InputSystem.OnInputReceived -= TransmitInput;

    // Movement
    private void TransmitInput(Vector2 inputVector) => targetMove.SetMovement(inputVector);
}
