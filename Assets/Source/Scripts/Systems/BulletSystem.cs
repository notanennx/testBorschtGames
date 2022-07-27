using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BulletSystem : MonoBehaviour
{
    // Awake
    private void Update()
    {
        // Flying bullets
        foreach (BulletComponent bullet in BulletComponent.Hashset)
            bullet.transform.Translate((transform.forward * (bullet.GetSpeed() * Time.deltaTime)), Space.Self);
    }
}
