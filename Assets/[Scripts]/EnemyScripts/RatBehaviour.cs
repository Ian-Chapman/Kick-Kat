using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBehaviour : MonoBehaviour
{
    public Animator ratAnimator;


    [Header("Movement")]
    public float runForce;


    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        ratAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveEnemy();
    }


    private void MoveEnemy()
    {
            rigidbody.AddForce(Vector3.forward * runForce);
            rigidbody.velocity *= 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // attack animation
        }
    }
}
