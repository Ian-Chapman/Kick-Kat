using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    float moveRateMultiplier = 1.0f;
    [SerializeField]
    AnimationCurve moveRateCurve;

    [SerializeField]
    Transform platform;
    private PlatformParenter platformParenter;
    [SerializeField]
    Transform endTransform;
    [SerializeField]
    float stoppingDistance;

    Vector3 startPos;
    Vector3 endPos;
    Vector3 targetDestination;

    float targetDistance;

    private bool movingToEnd;

    // Start is called before the first frame update
    void Start()
    {
        startPos = platform.position;
        endPos = endTransform.position;

        targetDistance = (endPos - startPos).magnitude;
        targetDestination = endPos;

        platformParenter = platform.GetComponent<PlatformParenter>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (platformParenter.player == null)
            Move();
        else
        {
            ThirdPersonMovement.controller.enabled = false;
            Move();
            ThirdPersonMovement.controller.enabled = true;
        }

        EndCheck();
    }

    private void Move()
    {
        Vector3 dir = (targetDestination - platform.position);
        float distance = dir.magnitude / targetDistance;
        dir.Normalize();

        // Movement  based on the move rate curve and multiplier
        platform.position += dir * moveRateCurve.Evaluate(distance) * moveRateMultiplier * Time.fixedDeltaTime;
    }

    private void EndCheck()
    {
        if ((platform.position - targetDestination).sqrMagnitude < stoppingDistance)
        {
            movingToEnd = !movingToEnd;

            if (movingToEnd)
                targetDestination = endPos;
            else
                targetDestination = startPos;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(platform.position, 0.5f);
        Gizmos.DrawWireSphere(endTransform.position, 0.5f);
    }
}
