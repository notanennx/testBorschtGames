using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class BulletComponent : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private int damage = 1;
    [SerializeField, BoxGroup("Main")] private float speed = 6.4f;

    // Hidden
    private Rigidbody rigidBody;

    // Setters
    public void SetDamage(int inputDamage) => damage = inputDamage;

    // Getters
    public float GetSpeed() => speed;

    // Hashset
    public static HashSet<BulletComponent> Hashset = new HashSet<BulletComponent>();

    // Filling
    private void OnEnable() => Hashset.Add(this);
    private void OnDisable() => Hashset.Remove(this);

    // Awaking
    private void Awake()
    {
        // Get
        rigidBody = GetComponent<Rigidbody>();
    }

    // Bullet impact
    private void OnTriggerEnter(Collider other)
    {
        // Enemy
        if (other.TryGetComponent(out HealthComponent victimHealth))
            victimHealth.TakeDamage(damage);

        // Destroy
        Destroy(gameObject);
    }
}
