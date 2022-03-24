using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleComponent : MonoBehaviour
{
    [SerializeField]
    private Gradient particleColor;
    [SerializeField]
    private string playerAttackTag;
    [SerializeField]
    private GameObject destructibleProp;
    [SerializeField]
    private List<ParticleSystem> particleSystems;
    private AudioSource audioSource;

    private bool isDestroyed = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDestroyed) return;

        // When the player enters the area, play the particle system and sound effect
        if (other.CompareTag(playerAttackTag))
        {
            if (particleSystems != null && particleSystems.Count > 0)
            {
                foreach (ParticleSystem ps in particleSystems)
                {
                    var main = ps.main;
                    main.startColor = particleColor;
                    ps.Play();
                }
            }

            if (audioSource != null)
            {
                audioSource.Play();
            }

            // Destroy Prop
            destructibleProp.SetActive(false);

            isDestroyed = true;
        }
    }
}
