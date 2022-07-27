using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class EnemyComponent : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private MeleeComponent melee;
    [SerializeField, BoxGroup("Main")] private VisionComponent vision;
    [SerializeField, BoxGroup("Main")] private Transform currentTarget;

    [SerializeField, BoxGroup("Settings")] private int minDamage;
    [SerializeField, BoxGroup("Settings")] private int maxDamage;

    // Hidden
    private float nextAttack;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private DeathComponent death;
    private PatrolComponent patrol;

    // Hashset
    public static HashSet<EnemyComponent> Hashset = new HashSet<EnemyComponent>();

    // Filling
    private void OnEnable() => Hashset.Add(this);
    private void OnDisable() => Hashset.Remove(this);

    // Getters
    public bool HasTarget() => (currentTarget != null);
    public Transform GetTarget() => currentTarget;
    public NavMeshAgent GetNavMeshAgent() => navMeshAgent;
    public MeleeComponent GetMelee() => melee;

    // Awaking
    private void Awake()
    {
        // Get
        death = GetComponent<DeathComponent>();
        patrol = GetComponent<PatrolComponent>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Subscribe
        vision.OnTargetFound += OnTargetFound;
    }

    // Target was found by vision
    private void OnTargetFound(Transform inputTarget)
    {
        // Exit
        if (death.IsDead()) return;
        if (currentTarget) return;

        // Setup
        patrol.SetPatrolling(false);
        navMeshAgent.ResetPath();
        currentTarget = inputTarget;
    }

    // Hit with melee attack!
    private void AttackHit()
    {
        // Exit
        if (death.IsDead()) return;
        if (!melee.GetTarget()) return;

        // Damage
        if (melee.GetTarget().TryGetComponent(out HealthComponent victimHealth))
            victimHealth.TakeDamage(UnityEngine.Random.Range(minDamage, maxDamage));
    }

    // Attack our enemy
    public void Attack()
    {
        // Exit
        if (death.IsDead()) return;
        if (nextAttack > Time.time) return;

        // Attack
        animator.SetTrigger("Attack");

        // Cooldown
        nextAttack = (Time.time + 2f);
    }

    // Makes enemy lose interest
    public void LoseInterest(Transform inputTarget)
    {
        // Exit
        if (currentTarget != inputTarget) return;

        // Setup
        patrol.SetPatrolling(true);
        navMeshAgent.ResetPath();
        currentTarget = null;
    }
}
