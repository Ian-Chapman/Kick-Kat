using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SidekickBehaviour : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;

    public GameObject followTarget;
    public NavMeshAgent brandon;

    // Start is called before the first frame update
    void Start()
    {
        followTarget = GameObject.Find("CatGirlChar");

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        if (followTarget.transform.position.x < 0.5f)
        {
            speed = 8;
            rotationSpeed = 3;
            brandon.SetDestination(followTarget.transform.position); //uses the nav mesh to chase the player
        }
        else if (followTarget.transform.position.x > 1)
        {
            speed = 0;
            rotationSpeed = 0;
        }
    }
}
