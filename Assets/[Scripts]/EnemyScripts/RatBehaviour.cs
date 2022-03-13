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

    

    public float speed = 1;
    

    // Start is called before the first frame update
    void Start()
    {
        ratAnimator = GetComponent<Animator>();

        patrolPointIndex = 0; //First partol point in index
        //transform.LookAt(patrolPoints[patrolPointIndex].position);
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
    }

    public void Patrol()
    {
        transform.LookAt(patrolPoints[patrolPointIndex].position);
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

}
