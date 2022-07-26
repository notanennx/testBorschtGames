using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class MoveComponent : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private float moveSpeed = 2.4f;
    [SerializeField, BoxGroup("Main")] private float rotateSpeed = 3.2f;

    // Hidden
    private bool isNpc;
    private Vector2 currentMovement;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private CharacterController charController;

    // Hashset
    public static HashSet<MoveComponent> Hashset = new HashSet<MoveComponent>();

    // Filling
    private void OnEnable() => Hashset.Add(this);
    private void OnDisable() => Hashset.Remove(this);

    // Getters
    public bool IsNpc() => isNpc;
    public bool IsMoving() => ((currentMovement.x != 0) || (currentMovement.y != 0));// || ((isNpc) && (navComponent.hasPath));
    public float GetMoveSpeed() => moveSpeed;
    public float GetRotationSpeed() => rotateSpeed;

    public Vector2 GetMovement() => currentMovement;
    public Animator GetAnimator() => animator;

    // Awaking
    private void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent)
        {
            isNpc = true;
            navMeshAgent.speed = moveSpeed;
        }

        charController = GetComponent<CharacterController>();
    }

    // Moves us somewhere
    public void Move(Vector3 newPosition) => charController.Move(newPosition * (moveSpeed * Time.deltaTime));

    // Sets a move speed
    public void SetSpeed(float inputSpeed)
    {
        // Set
        moveSpeed = inputSpeed;
        if (navMeshAgent)
            navMeshAgent.speed = inputSpeed;
    }

    // Sets a movement vector
    public void SetMovement(Vector2 inputMovement) => currentMovement = inputMovement;
}