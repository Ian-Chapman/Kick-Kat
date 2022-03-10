using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBehaviour : MonoBehaviour
{
    public Animator ratAnimator;
    public Transform player;

    public List<Transform> patrolPoints = new List <Transform>();

    private int patrolPointIndex;
    private float distToPatrolPoint;

    public float distanceToPlayer;

    public static float speed = 1;
    

    // Start is called before the first frame update
    void Start()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        ratAnimator = GetComponent<Animator>();

        patrolPointIndex = 0; //First partol point in index
        transform.LookAt(patrolPoints[patrolPointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        distToPatrolPoint = Vector3.Distance(transform.position, patrolPoints[patrolPointIndex].position);
        if (distToPatrolPoint < 1f)
        {
            IncreaseIndex();
        }
        Patrol();

        if (distanceToPlayer <= 2)
        {
            transform.LookAt(player.position);
            ratAnimator.SetBool("isPlayerInRange", true);
        }
    }

    void Patrol()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void IncreaseIndex()
    {
        patrolPointIndex++;

        if (patrolPointIndex >= patrolPoints.Count)
        {
            patrolPointIndex = 0; //reset the partol point to the first point in the array, which is 0.
        }
        transform.LookAt(patrolPoints[patrolPointIndex].position);
    }

    public void MoveToPlayer()
    {
        transform.LookAt(player.position);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    //private void OnTriggerEnter(Collider other)
    //{

    //    if (other.gameObject.tag == "Player")
    //    {

    //        speed = 3;
    //        ratAnimator.SetBool("isPlayerDetected", true);
    //        patrolPoints.Clear();
    //        //patrolPoints.Add(player);

    //        MoveToPlayer();


    //        if (distanceToPlayer <= 2)
    //        {
    //            transform.LookAt(player.position);
    //            ratAnimator.SetBool("isPlayerInRange", true);
    //        }

    //    }
    //}

}
