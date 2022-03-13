using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionE : MonoBehaviour
{
    public Transform player;
    public Transform ratE;
    public Animator ratAnimator;
    RatBehaviour ratBehaviour;

    public float distanceToPlayer;

    public List<Transform> patrolPoints = new List<Transform>();

    [Header("Patrol Points to Re-add")]
    public Transform patrolPointA;
    public Transform patrolPointB;

    // Start is called before the first frame update
    void Start()
    {
        ratBehaviour = GameObject.Find("RatE").GetComponent<RatBehaviour>();
        ratAnimator = GameObject.Find("RatE").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(ratE.transform.position, player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ratBehaviour.patrolPoints.Clear();
            ratBehaviour.patrolPoints.Add(player);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (distanceToPlayer >= 1.5f)
            {
                ratBehaviour.speed = 1;
                ratAnimator.SetBool("isWalking", true);
                ratAnimator.SetBool("isAttacking", false);
                ratBehaviour.MoveToPlayer();
            }

            else if (distanceToPlayer <= 1.5f)
            {
                ratBehaviour.speed = 0;
                ratAnimator.SetBool("isWalking", false);
                ratAnimator.SetBool("isAttacking", true);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If the player has left the same surface as the rat, the rat will resume its patrol
        if (other.gameObject.tag == "Player")
        {
            ratBehaviour.speed = 1;
            ratAnimator.SetBool("isAttacking", false);
            ratBehaviour.patrolPoints.Clear();
            ratBehaviour.patrolPoints.Add(patrolPointA);
            ratBehaviour.patrolPoints.Add(patrolPointB);
            ratAnimator.SetBool("isWalking", true);

            ratBehaviour.Patrol();
        }
    }
}
