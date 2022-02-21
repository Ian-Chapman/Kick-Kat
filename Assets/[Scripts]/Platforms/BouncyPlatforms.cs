using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyPlatforms : MonoBehaviour
{
    public float bounceHeight = 10.0f;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out ThirdPersonMovement movement))
        {
            // Bounce using the ThirdPersonMovement and Character Controller
            if (ThirdPersonMovement.playerVelocity.y < 0.0f)
            {
                ThirdPersonMovement.playerVelocity.y += bounceHeight;
                this.GetComponent<AudioSource>().Play();
            }
                
        }
    }
}
