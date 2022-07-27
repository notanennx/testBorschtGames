using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class DeathComponent : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private bool isDestroyingOnDeath;
    [SerializeField, BoxGroup("Main")] private Material deathMaterial;
    [SerializeField, BoxGroup("Main")] private SkinnedMeshRenderer meshRenderer;

    [SerializeField, BoxGroup("Main")] private bool canRespawn;
    [SerializeField, BoxGroup("Main"), ShowIf("canRespawn")] private Transform respawnPoint;

    // Hidden
    private bool isDead;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private LootComponent lootComponent;
    private HealthComponent healthComponent;
    private CharacterController characterController;

    // Actions
    public static Action<DeathComponent> OnDeath;

    // Gettes
    public bool IsDead() => isDead;

    // Awake
    private void Awake()
    {
        // Get
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        lootComponent = GetComponent<LootComponent>();
        healthComponent = GetComponent<HealthComponent>();
        characterController = GetComponent<CharacterController>();
    }

    // Catches death anim
    private void DeathComplete()
    {
        // Destroy
        if (isDestroyingOnDeath)
            Destroy(gameObject);
    }

    // Dies
    public void Die()
    {
        // Exit
        if (isDead) return;

        // Die
        isDead = true;
        animator.SetTrigger("Death");
        if (deathMaterial)
            meshRenderer.material = deathMaterial;

        // Loot
        if (lootComponent)
            lootComponent.CreateLoot();

        // Event
        OnDeath?.Invoke(this);

        // Disable
        characterController.enabled = false;
        if (navMeshAgent)
        {
            navMeshAgent.ResetPath();
            navMeshAgent.enabled = false;
        }
    }

    // Revives us
    [Button("Debug Respawn")]
    public void Respawn()
    {
        // Exit
        if (!canRespawn) return;

        // Respawn
        isDead = false;
        animator.Rebind();
        healthComponent.ResetHealth();
        characterController.enabled = true;

        // Change position
        if (respawnPoint)
            transform.position = respawnPoint.position;
    }
}
