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

    // Hidden
    private bool isDead;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
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
    [Button("Debug Revive")]
    public void Revive()
    {
        healthComponent.ResetHealth();
        characterController.enabled = true;
    }
}
