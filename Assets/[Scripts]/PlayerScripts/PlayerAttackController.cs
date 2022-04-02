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

    public CapsuleCollider rightFootCollider;
    public CapsuleCollider leftFootCollider;
    public BoxCollider rightPawCollider;
    public BoxCollider leftPawCollider;

    public bool isAttacking = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        rightFootCollider.enabled = false;
        leftFootCollider.enabled = false;
        rightPawCollider.enabled = false;
        leftPawCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //reset the clicks if the player has stopped clicking mid combo
        if (Time.time - prevClickTime > maxComboDelay) 
        {
            numOfClicks = 0;
            rightFootCollider.enabled = false;
        }

        if (Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer)
            return;

        if (Input.GetKeyDown(PlayerKeybinds.PlayerPunch))
        {
            

            prevClickTime = Time.time;
            numOfClicks++;

            if (numOfClicks == 1)
            {
                animator.SetBool("isKick1", true);
                rightFootCollider.enabled = true;
                PlayAttack();
            }
            //Max possible attacks in combo is clamped at 4
            numOfClicks = Mathf.Clamp(numOfClicks, 0, 4);
        }

        CheckIsAttacking();
    }

    // Keyframe functions
    public void return1()
    {
        if (numOfClicks >= 2)
        {
            animator.SetBool("isKick2", true);
            leftFootCollider.enabled = true;
            PlayAttack();
        }
        else
        {
            animator.SetBool("isKick1", false);
            rightFootCollider.enabled = false;
            isAttacking = false;
            numOfClicks = 0;
        }
    }

    public void return2()
    {
        if (numOfClicks >= 3)
        {
            animator.SetBool("isKick3", true);
            leftFootCollider.enabled = true;
            PlayAttack();
        }
        else
        {
            animator.SetBool("isKick2", false);
            leftFootCollider.enabled = false;
            isAttacking = false;
            numOfClicks = 0;
        }
    }

    public void return3()
    {
        if (numOfClicks >= 4)
        {
            animator.SetBool("isFinishingKick", true);
            rightFootCollider.enabled = true;
            PlayAttack();
        }
        else
        {
            animator.SetBool("isKick3", false);
            leftFootCollider.enabled = false;
            isAttacking = false;
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

    //Attack grunt sound effect
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

    /// UI Button Functions ///

    public void OnAttackButton_Pressed()
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

    public void CheckIsAttacking()
    {
        if ((rightFootCollider.enabled == true) || (leftFootCollider.enabled == true) ||
            (rightPawCollider.enabled == true) || (leftPawCollider.enabled == true))
        {
            isAttacking = true;
        }
        else
            isAttacking = false;

    }

}
