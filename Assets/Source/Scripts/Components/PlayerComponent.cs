using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerComponent : MonoBehaviour
{
    // Vars
    [SerializeField, BoxGroup("Main")] private AimingComponent aiming;
    [SerializeField, BoxGroup("Main")] private WeaponComponent weapon;
    [SerializeField, BoxGroup("Main")] private HealthbarComponent healthbar;

    // Getters
    public AimingComponent GetAiming() => aiming;
    public WeaponComponent GetWeapon() => weapon;
    public HealthbarComponent GetHealthbar() => healthbar;
}
