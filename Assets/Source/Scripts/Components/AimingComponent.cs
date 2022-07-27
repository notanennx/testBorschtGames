using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class AimingComponent : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private bool isAiming;
    [SerializeField, BoxGroup("Main")] private float aimingRange;
    [SerializeField, BoxGroup("Main")] private LayerMask targetLayer;

    [SerializeField, BoxGroup("Extra")] private WeaponComponent weapon;

    // Hashset
    public static HashSet<AimingComponent> Hashset = new HashSet<AimingComponent>();

    // Hidden
    private float nextTargetSearch;
    private Animator animator;
    private Transform currentTarget;
    private DeathComponent death;

    // Awaking
    private void Awake()
    {
        // Set
        death = GetComponent<DeathComponent>();
        animator = GetComponent<Animator>();

        // Subscribe
        //death.
    }

    // Filling
    private void OnEnable() => Hashset.Add(this);
    private void OnDisable() => Hashset.Remove(this);

    // Enables/disables aiming
    public void SetAiming(bool inputAiming)
    {
        isAiming = inputAiming;
    }

    // Finds and sets a target
    public void FindSetTarget()
    {
        // Exit
        if (nextTargetSearch > Time.time) return;

        // Set
        currentTarget = GetClosestTarget();

        // Cooldown
        nextTargetSearch = (Time.time + 0.2f);
    }

    // Process aiming
    public void ProcessAiming()
    {
        // Exit
        if (!isAiming) return;
        if (death.IsDead()) return;

        // Moving break
        if ((animator.GetBool("IsMoving")) || (!currentTarget))
        {
            weapon.Cooldown(0.4f);
            animator.SetBool("IsAiming", false);
            return;
        }

        // Aiming
        animator.SetBool("IsAiming", isAiming);
        if (weapon)
            weapon.Shoot();

        // Direction
        Vector3 newDirection = (currentTarget.position - transform.position);
        Vector3 smoothDirection = Vector3.RotateTowards(transform.forward, newDirection, (8f * Time.deltaTime), 0f);

        // Rotation
        Quaternion targetRotation = Quaternion.LookRotation(smoothDirection);
            targetRotation.x = 0;
            targetRotation.z = 0;

        // Rotating it
        transform.rotation = targetRotation;
    }

    // Gets a closest target
    public Transform GetClosestTarget()
    {
        float minDist = Mathf.Infinity;

        Vector3 currentPos = transform.position;
        Transform targetClose = null;
        foreach (EnemyComponent target in EnemyComponent.Hashset)
        {
            float dist = Vector3.Distance(target.transform.position, currentPos);
            if ((dist < minDist) && (dist < aimingRange))
            {
                minDist = dist;
                targetClose = target.transform;
            }
        }
        return targetClose;
    }
}
