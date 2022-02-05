using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayBottleScript : MonoBehaviour
{
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private ParticleSystem sprayParticleSystem;

    private void OnTriggerEnter(Collider other)
    {
        // When the player enters the area, play spray animation and apply damage
        if (other.gameObject.TryGetComponent(out ThirdPersonMovement movement))
        {
            sprayParticleSystem.Play();
        }
    }
}
