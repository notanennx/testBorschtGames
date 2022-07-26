using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MoveSystem : MonoBehaviour
{
    // Update
    private void Update()
    {
        foreach (MoveComponent moveComponent in MoveComponent.Hashset)
        {
            if (moveComponent.IsMoving())
            {
                // Non npcs only
                if (!moveComponent.IsNpc())
                {
                    // Get
                    Vector3 newPosition = new Vector3(moveComponent.GetMovement().x, 0f, moveComponent.GetMovement().y).normalized;
                    Vector3 newDirection = Vector3.RotateTowards(moveComponent.transform.forward, newPosition, (moveComponent.GetRotationSpeed() * Time.deltaTime), 0f);
                
                    // Angle
                    Quaternion targetRotation = Quaternion.LookRotation(newDirection);
                        targetRotation.x = 0;
                        targetRotation.z = 0;

                    // Movement
                    moveComponent.transform.rotation = targetRotation;
                    moveComponent.Move(newPosition);
                }

                // Animation
                moveComponent.GetAnimator().SetBool("IsMoving", true);
            }
            else
            {
                // Animation
                moveComponent.GetAnimator().SetBool("IsMoving", false);
            }
        }
    }
}
