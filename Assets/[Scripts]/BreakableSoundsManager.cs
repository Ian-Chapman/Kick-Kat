using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableSoundsManager : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> breakableSoundClips;

    [SerializeField]
    public AudioSource audioSource;

    [SerializeField]
    public float pitchVariance = .15f;

    public void PlayBreakableSound()
    {
        audioSource.clip = breakableSoundClips[Random.Range(0, breakableSoundClips.Count)];
        audioSource.pitch = 1.0f + Random.Range(-pitchVariance, pitchVariance);
        audioSource.Play();
    }


}
