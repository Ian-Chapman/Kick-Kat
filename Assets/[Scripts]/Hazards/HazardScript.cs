using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardScript : MonoBehaviour
{
    [SerializeField]
    private List<ParticleSystem> hazardParticleSystems;

    public GameObject player;

    private void Start()
    {
        player = GameObject.Find("CatGirlChar");
    }

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

            if (this.tag == "Glass")
            {
                this.GetComponent<AudioSource>().Play();
            }
        }
    }
}
