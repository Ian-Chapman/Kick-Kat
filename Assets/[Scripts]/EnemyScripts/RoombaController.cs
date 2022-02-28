using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoombaController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;

    public GameObject player;
    public NavMeshAgent roomba;

    public float playerPos;

    void Start()
    {
        player = GameObject.Find("CatGirlChar");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        if (player.transform.position.y < 0.5f)
        {
            //isCleaning = true;
            speed = 8;
            rotationSpeed = 3;
            roomba.SetDestination(player.transform.position); //uses the nav mesh to chase the player
        }
        else if (player.transform.position.y > 1)
        {
            //isCleaning = false;
            speed = 0;
            rotationSpeed = 0;
        }
    }
}
