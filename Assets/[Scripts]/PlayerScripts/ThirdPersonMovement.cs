using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (CharacterController))]
public class ThirdPersonMovement : MonoBehaviour
{
    public Animator animator;

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
    void Update()
    {
        carmellaAnimationManager.animInput();

        moveVector = Vector3.zero;

       
        float horizonal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
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

        if (Input.GetKeyDown("x"))
        {
            UseItem(new Item { itemType = Item.ItemType.tempPup, amount = 1 });
        }

        jump();
    }

    public void jump()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Changes the height position of the player..
        if (Input.GetKeyDown(PlayerKeybinds.PlayerJump) && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -1.8f * gravityValue);
            audioSource.clip = audioClips[4];
            audioSource.Play();
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
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
            StartCoroutine(TakeDamage());
        }

        if ((other.gameObject.tag == "StoveBurner"))
        {
            audioSource.clip = audioClips[2];
            audioSource.Play();
            StartCoroutine(TakeDamage());
        }
    }
    
    private void UseItem(Item item)
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


    public IEnumerator TakeDamage()
    {

        animator.SetBool("isDamaged", true);        
        yield return new WaitForSeconds(.6f);
        animator.SetBool("isDamaged", false);
        yield return new WaitForSeconds(.2f);
        this.transform.position = new Vector3(20.3f, 1.7f, -15.456f);
        //add life reduction here
    }


}
