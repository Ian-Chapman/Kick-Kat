using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


[RequireComponent(typeof (CharacterController))]
public class ThirdPersonMovement : MonoBehaviour
{
    public Animator animator;
    public Joystick rightHandJoystick;
    public Joystick leftHandJoystick;

    private Joystick CurrentJoystick
    {
        get
        {
            return (PlayerKeybinds.PlayerRightHanded ? rightHandJoystick : leftHandJoystick);
        }
    }

    Vector3 moveVector;
    static public CharacterController controller;
    public Transform cam;
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    [SerializeField]
    static public float speed = 6f;
    static public Vector3 playerVelocity;

    static public bool groundedPlayer;
    private float playerSpeed = 2.0f;
    static public float jumpHeight = 10.0f;
    static public float gravityValue = -9.81f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    CarmellaAnimationManager carmellaAnimationManager;
    RoombaController roombaController;

    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory;

    public IEnumerator deathCoroutine;

    public int lives = 9;

    public TextMeshProUGUI livesText;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        carmellaAnimationManager = gameObject.GetComponent<CarmellaAnimationManager>();
        roombaController = gameObject.GetComponent<RoombaController>();;


        inventory = new Inventory(UseItem);
        uiInventory.SetInventory(inventory);

        ItemWorld.SpawnItemWorld(new Vector3(-3, 2, 3), new Item { itemType = Item.ItemType.Points, amount = 1 });
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //carmellaAnimationManager.animInput();

        moveVector = Vector3.zero;

        float horizonal = Input.GetAxisRaw("Horizontal") + CurrentJoystick.Horizontal;
        float vertical = Input.GetAxisRaw("Vertical") + CurrentJoystick.Vertical;

        Vector3 direction = new Vector3(horizonal, 0f, vertical).normalized; //normalized prevents double key inputs from making player move faster

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        //apply gravity to the character controller. Rigidbody will not have any effect if character contoller is being used.
        if (controller.isGrounded == false)
        {
            moveVector += Physics.gravity;
        }
        
        controller.Move(moveVector * Time.deltaTime);

        //if (Input.GetKeyDown("x"))
        //{
        //    UseItem(new Item { itemType = Item.ItemType.tempPup, amount = 1 });
        //}

        playerVelocity.y += gravityValue * Time.fixedDeltaTime;
        controller.Move(playerVelocity * Time.fixedDeltaTime);

        if (lives <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }

    public void jump()
    {
        groundedPlayer = controller.isGrounded;

        // Changes the height position of the player..
        if (groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -1.8f * gravityValue);
            audioSource.clip = audioClips[4];
            audioSource.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ItemWorld itemWorld = other.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }

        if ((other.gameObject.tag == "Roomba"))
        {
            audioSource.clip = audioClips[1];
            audioSource.Play();
            StartTakeDamage();
        }

        if ((other.gameObject.tag == "StoveBurner"))
        {
            
            audioSource.clip = audioClips[2];
            audioSource.Play();
            StartTakeDamage();
        }

        if ((other.gameObject.layer == LayerMask.NameToLayer("Hazard")))
        {
            StartTakeDamage();
        }
    }
    
    public void UseItem(Item item)
    {
        
        switch (item.itemType)
        {
            case Item.ItemType.Points:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Points, amount = 1 });
                // add behaviour here
                break;

            case Item.ItemType.PowerUp:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.PowerUp, amount = 1 });
                // add behaviour here
                break;

            case Item.ItemType.tempPup:
                inventory.RemoveItem(new Item { itemType = Item.ItemType.tempPup, amount = 1 });
                // add behaviour here
                break;
        }
    }

    public void StartTakeDamage()
    {
        if (deathCoroutine != null)
            return;

        deathCoroutine = TakeDamage();
        StartCoroutine(deathCoroutine);
        lives--;
        livesText.text = "x" + lives.ToString();
    }

    public IEnumerator TakeDamage()
    {
        animator.SetBool("isDamaged", true);
        yield return new WaitForSeconds(0.5f); //animation duration should match this timing
        animator.SetBool("isDamaged", false);

        GetComponent<CharacterController>().enabled = false;
        this.transform.position = new Vector3(20.3f, 1.7f, -15.456f); //Players starting position in the level
        GetComponent<CharacterController>().enabled = true;

        deathCoroutine = null;
    }

    /// UI Button Functions ///

    public void OnJumpButton_Pressed()
    {
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -1.8f * gravityValue);
            audioSource.clip = audioClips[4];
            audioSource.Play();
        }
    }
}
