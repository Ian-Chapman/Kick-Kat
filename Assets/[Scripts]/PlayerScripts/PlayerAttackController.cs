using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public Animator animator;
    public int numOfClicks;
    float prevClickTime;
    public float maxComboDelay = 0.8f;
    private int prevSound, currSound = -1;

    public AudioClip[] audioClips;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //reset the clicks if the player has stopped clicking mid combo
        if (Time.time - prevClickTime > maxComboDelay) 
        {
            numOfClicks = 0;
        }

        if (Input.GetKeyDown(PlayerKeybinds.PlayerPunch))
        {
            prevClickTime = Time.time;
            numOfClicks++;

            if (numOfClicks == 1)
            {
                animator.SetBool("isKick1", true);
                PlayAttack();
            }
            //Max possible attacks in combo is clamped at 4
            numOfClicks = Mathf.Clamp(numOfClicks, 0, 4);
        }
    }

    // Keyframe functions
    public void return1()
    {
        if (numOfClicks >= 2)
        {
            animator.SetBool("isKick2", true);
            PlayAttack();
        }
        else
        {
            animator.SetBool("isKick1", false);
            numOfClicks = 0;
        }
    }

    public void return2()
    {
        if (numOfClicks >= 3)
        {
            animator.SetBool("isKick3", true);
            PlayAttack();
        }
        else
        {
            animator.SetBool("isKick2", false);
            numOfClicks = 0;
        }
    }

    public void return3()
    {
        if (numOfClicks >= 4)
        {
            animator.SetBool("isFinishingKick", true);
            PlayAttack();
        }
        else
        {
            animator.SetBool("isKick3", false);
            numOfClicks = 0;
        }
    }

    public void return4()
    {
        animator.SetBool("isKick1", false);
        animator.SetBool("isKick2", false);
        animator.SetBool("isKick3", false);
        animator.SetBool("isFinishingKick", false);
        numOfClicks = 0;
    }

    public void PlayAttack()
    {
        prevSound = currSound;
        currSound = Random.Range(0, 5);
        if (currSound == prevSound)
        {
            currSound = prevSound;
            PlayAttack();
        }
        else
        {
            audioSource.clip = audioClips[currSound];
            audioSource.Play();
        }
    }
}
