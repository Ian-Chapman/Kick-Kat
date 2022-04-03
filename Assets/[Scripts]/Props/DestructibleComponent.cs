using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleComponent : MonoBehaviour
{


    [SerializeField]
    private int objectHealth = 1;
    [SerializeField]
    private int scoreValue = 50;

    [SerializeField]
    private Gradient particleColor;
    //[SerializeField]
    //private string playerAttackTag;
    [SerializeField]
    private GameObject destructibleProp;
    [SerializeField]
    private List<ParticleSystem> particleSystems;
    [SerializeField]
    private ParticleSystem bubbleSystem;
   
    [SerializeField]
    List<AudioClip> breakableSoundClips;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    public float pitchVariance = .15f;

    [SerializeField]
    private SpeechBubbleSet bubbleSet;

    private bool isDestroyed = false;

    private void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDestroyed) return;

        // When the player enters the area, play the particle system and sound effect
        if (other.CompareTag("Right Foot") || other.CompareTag("Left Foot") ||
            other.CompareTag("Right Paw") || other.CompareTag("Left Paw"))
        {
            if (particleSystems != null && particleSystems.Count > 0)
            {
                foreach (ParticleSystem ps in particleSystems)
                {
                    // Particle Color
                    var main = ps.main;
                    main.startColor = particleColor;

                    ps.Play();
                }
            }

            if (bubbleSystem != null && bubbleSet != null)
            {
                bubbleSystem.GetComponent<ParticleSystemRenderer>().material = bubbleSet.GetRandomBubble();
                bubbleSystem.Play();
            }

            if (--objectHealth > 0) return;

            if (audioSource != null)
            {
                //if (audioSource.isPlaying)
                //    audioSource.Stop();
                //else
                //    audioSource.Play();
                PlayBreakableSound();
            }

            // Destroy Prop
            destructibleProp.SetActive(false);
            if (ScoreManager.Instance != null)
                ScoreManager.Instance.IncreaseScore(scoreValue);
            isDestroyed = true;
        }
    }

    public void PlayBreakableSound()
    {
        audioSource.clip = breakableSoundClips[Random.Range(0, breakableSoundClips.Count)];
        audioSource.pitch = 1.0f + Random.Range(-pitchVariance, pitchVariance);
        audioSource.Play();
    }
}
