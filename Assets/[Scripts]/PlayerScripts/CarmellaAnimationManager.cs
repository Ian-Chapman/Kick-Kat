using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarmellaAnimationManager : MonoBehaviour
{
    PlayerAttackController playerAttackController;

    public CapsuleCollider leftFootCollider;
    public CapsuleCollider rightFootCollider;
    public BoxCollider leftPawCollider;
    public BoxCollider rightPawCollider;

    ThirdPersonMovement thirdPersonMovement;
    public Joystick rightHandJoystick;
    public Joystick leftHandJoystick;

    private Joystick CurrentJoystick
    {
        get
        {
            return (PlayerKeybinds.PlayerRightHanded ? rightHandJoystick : leftHandJoystick);
        }
    }

    static public Rigidbody body;
    static Animator animator;
    //int isWalkingHash;
    
    public static bool isInAir = false;

    private Vector3 m_vPrevVel;
    private Vector3 m_vDirection;

    //bool isRunPunch = false;
    //bool isKick1 = false;
    //bool isKick2 = false;
    //bool isKick3 = false;
    //bool isFinishingKick = false;



    private void Awake()
    {
        //leftFootCollider.enabled = false;
        //rightFootCollider.enabled = false;
        leftPawCollider.enabled = false;
        rightPawCollider.enabled = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        thirdPersonMovement = gameObject.GetComponent<ThirdPersonMovement>();
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerAttackController = gameObject.GetComponent<PlayerAttackController>();
    }

    void FixedUpdate()
    {
        animInput();
    }


    public void animInput()
    {
        bool isIdle = animator.GetBool("isIdle");
        bool isJumpingForward = animator.GetBool("isJumpingForward");
        bool isRunning = animator.GetBool("isRunning");
        bool isJumping = animator.GetBool("isJumping");
        bool isFalling = animator.GetBool("isFalling");

        bool forwardPressed = Input.GetKey("w");
        bool backPressed = Input.GetKey("s");
        bool rightPressed = Input.GetKey("d");
        bool leftPressed = Input.GetKey("a");
        bool jumpPressed = Input.GetKey(PlayerKeybinds.PlayerJump);

        // Joystick input
        forwardPressed |= CurrentJoystick.Vertical > 0.1f;
        backPressed |= CurrentJoystick.Vertical < -0.1f;
        rightPressed |= CurrentJoystick.Horizontal > 0.1f;
        leftPressed |= CurrentJoystick.Horizontal < -0.1f;

        //bool runningPunch 




        //------------------------------------------------------------------------ Ground Locomotion ------------------------------------------------------
        //Idle
        if ((!forwardPressed || 
            !backPressed || !rightPressed || !leftPressed || !jumpPressed))
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
            ThirdPersonMovement.speed = 0;
        }

        if (isIdle)
        {
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
            Debug.Log("Jumped");
            animator.SetBool("isJumping", true);
            animator.SetBool("isFalling", false);
        }

        if (!ThirdPersonMovement.groundedPlayer)
        {
            //animator.SetBool("isJumping", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isFalling", true);
            animator.SetBool("isIdle", false);

        }

        if (jumpPressed && !ThirdPersonMovement.groundedPlayer)
        {
            animator.SetBool("isFalling", true);
        }



                //Jumping in place
                //if (jumpPressed && ThirdPersonMovement.groundedPlayer && !isRunning)
                //{
                //    thirdPersonMovement.jump();
                //    animator.SetBool("isJumping", true);
                //}
                //else if (!jumpPressed && !ThirdPersonMovement.groundedPlayer)
                //    animator.SetBool("isJumping", false);

                ThirdPersonMovement.playerVelocity.y += ThirdPersonMovement.gravityValue * Time.deltaTime;
        ThirdPersonMovement.controller.Move(ThirdPersonMovement.playerVelocity * Time.deltaTime);





        //------------------------------------------------------------------ Melee Combat Moves --------------------------------------------------

        //Running Punch
        if (((forwardPressed || backPressed || rightPressed || leftPressed)) && Input.GetKeyDown(PlayerKeybinds.PlayerPunch))
        {
            animator.SetBool("isRunPunch", true);
            leftPawCollider.enabled = true;
        }
        else
            animator.SetBool("isRunPunch", false);
            leftPawCollider.enabled = false;

        //Running Kick
        if (((forwardPressed || backPressed || rightPressed || leftPressed)) && Input.GetKeyDown(PlayerKeybinds.PlayerKick))
        {
            animator.SetBool("isRunKick", true);
            leftFootCollider.enabled = true;
        }
        else
        {
            animator.SetBool("isRunKick", false);
        }

    }

    /// UI Button Functions ///

    public void OnJumpButton_Pressed()
    {
        animator.SetBool("isJumping", true);
    }
}

