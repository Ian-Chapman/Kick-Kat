using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyPlatforms : MonoBehaviour
{
    public Animator playerAnimator;
    public float bounceHeight = 10.0f;

    private void Start()
    {
        playerAnimator = GameObject.Find("CatGirlChar").GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out ThirdPersonMovement movement))
        {
            // Bounce using the ThirdPersonMovement and Character Controller
            if (ThirdPersonMovement.playerVelocity.y < 0.0f)
            {
                playerAnimator.SetBool("isFalling", true); // play falling animation
                ThirdPersonMovement.playerVelocity.y += bounceHeight;
                this.GetComponent<AudioSource>().Play();
            }


        }
    }
}
