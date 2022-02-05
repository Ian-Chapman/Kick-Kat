using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarmellaAnimationManager : MonoBehaviour
{
    ThirdPersonMovement thirdPersonMovement;

    static public Rigidbody body;
    static Animator animator;
    //int isWalkingHash;
    
    public static bool isInAir = false;

    private Vector3 m_vPrevVel;
    private Vector3 m_vDirection;

    bool isRunPunch = false;
    bool isKick1 = false;
    bool isKick2 = false;
    bool isKick3 = false;
    bool isFinishingKick = false;


    // Start is called before the first frame update
    void Start()
    {
        thirdPersonMovement = gameObject.GetComponent<ThirdPersonMovement>();
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        //isWalkingHash = Animator.StringToHash("isWalking");
    }

    void Update()
    {

        float velocity = Mathf.Abs(body.velocity.x);
        m_vPrevVel = body.velocity;
    }


    public void animInput()
    {
        bool isIdle = animator.GetBool("isIdle");
        bool isJumpingForward = animator.GetBool("isJumpingForward");
        bool isRunning = animator.GetBool("isRunning");
        //bool isJumping = animator.GetBool("isJumping");
        bool isFalling = animator.GetBool("isFalling");

        bool forwardPressed = Input.GetKey("w");
        bool backPressed = Input.GetKey("s");
        bool rightPressed = Input.GetKey("d");
        bool leftPressed = Input.GetKey("a");
        bool jumpPressed = Input.GetKey("space");
        
        //bool runningPunch 


        //------------------------------------------------------------------------ Ground Locomotion ------------------------------------------------------
        //Idle
        if ((!forwardPressed || 
            !backPressed || !rightPressed || !leftPressed || !jumpPressed))
        {
            animator.SetBool("isIdle", true);
            ThirdPersonMovement.speed = 0;
        }

        //Running
        if ((forwardPressed || backPressed || rightPressed || leftPressed)) //if player is running
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdle", false);
            ThirdPersonMovement.speed = 10; // Run movement speed
        }
        else
        {
            animator.SetBool("isRunning", false);
            ThirdPersonMovement.speed = 0;
        }

        //Prevent animation and movement if keys are pressed at same time
        if ((forwardPressed && backPressed) || (rightPressed && leftPressed))
        {
            animator.SetBool("isRunning", false);
            ThirdPersonMovement.speed = 0;
        }


        ThirdPersonMovement.groundedPlayer = ThirdPersonMovement.controller.isGrounded;
        if (ThirdPersonMovement.groundedPlayer && ThirdPersonMovement.playerVelocity.y < 0)
        {
            ThirdPersonMovement.playerVelocity.y = 0f;
        }


        //Changes the height position of the player..
        if (jumpPressed && ThirdPersonMovement.groundedPlayer)
        {
            //ThirdPersonMovement.playerVelocity.y += Mathf.Sqrt(ThirdPersonMovement.jumpHeight * -3.0f * ThirdPersonMovement.gravityValue);
            thirdPersonMovement.jump();
            animator.SetBool("isJumpingForward", true);
        }
        else
            animator.SetBool("isJumpingForward", false);

        ThirdPersonMovement.playerVelocity.y += ThirdPersonMovement.gravityValue * Time.deltaTime;
        ThirdPersonMovement.controller.Move(ThirdPersonMovement.playerVelocity * Time.deltaTime);

        //------------------------------------------------------------------ Melee Combat Moves --------------------------------------------------

        //Running Punch
        if (((forwardPressed || backPressed || rightPressed || leftPressed)) && Input.GetButton("Fire1"))
        {
            animator.SetBool("isRunPunch", true);
        }
        else
            animator.SetBool("isRunPunch", false);

        //Running Kick
        if (((forwardPressed || backPressed || rightPressed || leftPressed)) && Input.GetButton("Fire2"))
        {
            animator.SetBool("isRunKick", true);
        }
        else
            animator.SetBool("isRunKick", false);


        StartCoroutine(KickCombo());

    }

    //Chaining kicking combos together
    IEnumerator KickCombo()
    {
        isKick1 = true;

        if (Input.GetButton("Fire1") && isKick1 == true)
        {
            animator.SetBool("isKick1", true);
            yield return new WaitForSeconds(0.5f);
            animator.SetBool("isKick1", false);
            isKick2 = true;
        }

        if (Input.GetButton("Fire1") && isKick2 == true)
        {
            animator.SetBool("isKick2", true);
            yield return new WaitForSeconds(0.6f);
            animator.SetBool("isKick2", false);
            isKick3 = true;
        }

        if (Input.GetButton("Fire1") && isKick3 == true)
        {
            animator.SetBool("isKick3", true);
            yield return new WaitForSeconds(0.6f);
            animator.SetBool("isKick3", false);
            isFinishingKick = true;
        }

        if (Input.GetButton("Fire1") && isFinishingKick == true)
        {
            animator.SetBool("isFinishingKick", true);
            yield return new WaitForSeconds(0.6f);
            animator.SetBool("isFinishingKick", false);
            isKick1 = false;
            isKick2 = false;
            isKick3 = false;
            isFinishingKick = false;
        }
        
    }
}

