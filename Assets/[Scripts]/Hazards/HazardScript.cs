using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardScript : MonoBehaviour
{
    [SerializeField]
    private List<ParticleSystem> hazardParticleSystems;

    private void OnTriggerEnter(Collider other)
    {
        // When the player enters the area, play the hazard particle system (Spray bottle water, smoke, etc.)
        if (other.gameObject.TryGetComponent(out ThirdPersonMovement movement))
        {
            if (hazardParticleSystems != null && hazardParticleSystems.Count > 0)
            {
                foreach (ParticleSystem ps in hazardParticleSystems)
                {
                    ps.Play();
                }
            }
        }
    }
}
