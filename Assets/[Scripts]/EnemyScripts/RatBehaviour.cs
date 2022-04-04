using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBehaviour : MonoBehaviour
{
    public Animator ratAnimator;
    public Transform player;

    [SerializeField]
    public AudioSource audioSource;
    [SerializeField]
    public List<AudioClip> clips;
    [SerializeField]
    public float pitchVariance = .15f;

    public List<Transform> patrolPoints = new List <Transform>();

    private int patrolPointIndex;
    private float distToPatrolPoint;

    public int ratHealth = 2;

    public float speed = 1;



    // Start is called before the first frame update
    void Start()
    {
        ratAnimator = GetComponent<Animator>();
        patrolPointIndex = 0; //First partol point in index
        audioSource = GetComponent <AudioSource>();
        //transform.LookAt(patrolPoints[patrolPointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        CheckRatHealth();

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

    public void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Right Foot") || (other.gameObject.tag == "Left Foot") ||
            (other.gameObject.tag == "Right Paw") || (other.gameObject.tag == "Left Paw"))
        {
            ratHealth--;
            PlayHitSound();
            Debug.Log(ratHealth);
        }

        if (other.gameObject.tag == "Floor")
        {
            ratHealth = 0;
        }
    }

    void CheckRatHealth()
    {
        if (ratHealth <= 0)
        {
            StartCoroutine(RatDeath());
            PlayDeathSound();
        }
    }

    IEnumerator RatDeath()
    {
        //yield return new WaitForSeconds(1.5f);
        ratAnimator.SetBool("isDying", true);
        yield return new WaitForSeconds(1.5f);
        //RemoveRatPatrolPoints();
        Destroy(gameObject);
    }

    //public void RemoveRatPatrolPoints()
    //{
    //    if (Rat = null)
    //    {
    //        RatAPatrolPoints.SetActive(false);
    //    }

    //    if (RatB = null)
    //    {
    //        RatBPatrolPoints.SetActive(false);
    //    }

    //    if (RatC = null)
    //    {
    //        RatCPatrolPoints.SetActive(false);
    //    }

    //    if (RatD = null)
    //    {
    //        RatDPatrolPoints.SetActive(false);
    //    }

    //    if (RatE = null)
    //    {
    //        RatEPatrolPoints.SetActive(false);
    //    }
    //}

    public void PlayDeathSound()
    {
        audioSource.clip = clips[1];
        audioSource.Play();
    }

    public void PlayHitSound()
    {
        audioSource.clip = clips[0];
        audioSource.pitch = 1.0f + Random.Range(-pitchVariance, pitchVariance);
        audioSource.Play();
    }

}
