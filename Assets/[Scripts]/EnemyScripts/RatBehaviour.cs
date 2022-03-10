using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBehaviour : MonoBehaviour
{
    public Animator ratAnimator;

    public Transform[] patrolPoints;

    private int patrolPointIndex;
    private float distToPatrolPoint;


    [Header("Movement")]
    public float speed;
    

    // Start is called before the first frame update
    void Start()
    {
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
    }

    void Patrol()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void IncreaseIndex()
    {
        patrolPointIndex++;

        if (patrolPointIndex >= patrolPoints.Length)
        {
            patrolPointIndex = 0; //reset the partol point to the first point in the array, which is 0.
        }
        transform.LookAt(patrolPoints[patrolPointIndex].position);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // attack animation
        }
    }
}
