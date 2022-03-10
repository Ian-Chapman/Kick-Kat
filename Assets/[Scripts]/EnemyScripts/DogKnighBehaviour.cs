using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogKnighBehaviour : MonoBehaviour
{
    public Animator animator;
    private bool isAngry;
    public GameObject target;

    void Start()
    {
        isAngry = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (target.GetComponent<Transform>().position.x > -1.5f && target.GetComponent<Transform>().position.x < 3)
            Stalk();
    }

    private void Stalk()
    {
        this.GetComponent<Transform>().position = new Vector3(target.GetComponent<Transform>().position.x, this.GetComponent<Transform>().position.y, this.GetComponent<Transform>().position.z);
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "Player")
        {
            if (isAngry)
                animator.SetTrigger("isInRange");
        }

    }
}
