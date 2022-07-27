using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class WeaponComponent : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private Transform modelTransform;
    [SerializeField, BoxGroup("Main")] private Transform shootPosTransform;
    [SerializeField, BoxGroup("Main")] private GameObject bulletTemplate;

    [SerializeField, BoxGroup("Settings")] private int damage;
    [SerializeField, BoxGroup("Settings")] private float cooldown;

    // Hidden
    private float nextShoot;

    // Awaking
    private void Awake()
    {
        // Set
        modelTransform.localScale = Vector3.zero;
    }

    // Shoots the bullet
    [Button("Debug Shoot")]
    public void Shoot()
    {
        // Exit
        if (nextShoot > Time.time) return;

        // Create bullet and etc
        BulletComponent newBullet = Instantiate(bulletTemplate, shootPosTransform.position, transform.rotation).GetComponent<BulletComponent>();
            newBullet.SetDamage(damage);

        // Cooldown
        Cooldown(cooldown);
    }

    // Shows a gun
    public void Show()
    {
        modelTransform.DOKill();
        modelTransform.DOScale(Vector3.one, 0.3f);
    }

    // Hides a gun
    public void Hide()
    {
        modelTransform.DOKill();
        modelTransform.DOScale(Vector3.zero, 0.3f);
    }

    // Cooldowns shooting
    public void Cooldown(float inputCooldown)
    {
        nextShoot = (Time.time + inputCooldown);
    }
}
