using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScript : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem splashEffect;

    private GameObject player;

    private void Start()
    {
        player = FindObjectOfType<ThirdPersonMovement>().gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        splashEffect.transform.position = GetComponent<Collider>().ClosestPoint(player.transform.position);
    }
}
