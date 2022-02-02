using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (CharacterController))]
public class ThirdPersonMovement : MonoBehaviour
{
    Vector3 moveVector;
    static public CharacterController controller;
    public Transform cam;

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

    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        carmellaAnimationManager = gameObject.GetComponent<CarmellaAnimationManager>();

        inventory = new Inventory();
        uiInventory.SetInventory(inventory);

        ItemWorld.SpawnItemWorld(new Vector3(-3, 2, 3), new Item { itemType = Item.ItemType.Points, amount = 1 });
    }

    private void OnTriggerEnter(Collider other)
    {
        ItemWorld itemWorld = other.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
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
            foreach (Item item in inventory.GetItemList())
                inventory.RemoveItem(item);
        }

        //jump();
    }

    // still a bit buggy, but its working
    //public void jump()
    //{
    //    groundedPlayer = controller.isGrounded;
    //    if (groundedPlayer && playerVelocity.y < 0)
    //    {
    //        playerVelocity.y = 0f;
    //    }


    //    // Changes the height position of the player..
    //    if (Input.GetButtonDown("Jump") && groundedPlayer)
    //    {
    //        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    //    }

    //    playerVelocity.y += gravityValue * Time.deltaTime;
    //    controller.Move(playerVelocity * Time.deltaTime);
    //}

}
