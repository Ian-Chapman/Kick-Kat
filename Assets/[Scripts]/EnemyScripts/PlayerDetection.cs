using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public Transform player;
    public Transform rat;
    public Animator ratAnimator;
    RatBehaviour ratBehaviour;

    public float distanceToPlayer;

    public List<Transform> patrolPoints = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        ratBehaviour = GameObject.Find("Rat").GetComponent<RatBehaviour>();
        ratAnimator = GameObject.Find("Rat").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(rat.transform.position, player.transform.position);

        if (distanceToPlayer <= 2)
        {
            transform.LookAt(player.position);
            ratAnimator.SetBool("isPlayerInRange", true);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ratBehaviour.speed = 3;
            ratAnimator.SetBool("isPlayerDetected", true);
            ratBehaviour.patrolPoints.Clear();
            ratBehaviour.patrolPoints.Add(player);

            ratBehaviour.MoveToPlayer(); //not sure if I need this
            ratBehaviour.Patrol();

            if (distanceToPlayer <= 2)
            {
                transform.LookAt(player.position);
                ratAnimator.SetBool("isPlayerInRange", true);
            }

        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        ratBehaviour.speed = 1;
    //        ratAnimator.SetBool("isPlayerDetected", false);
    //        ratAnimator.SetBool("isPlayerInRange", false);
    //        ratBehaviour.patrolPoints.Clear();
    //        ratBehaviour.patrolPoints.Add(patrolPoints);

    //        ratBehaviour.Patrol();
    //    }
    //}
}
