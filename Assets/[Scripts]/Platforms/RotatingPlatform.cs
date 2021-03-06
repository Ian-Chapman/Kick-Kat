using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private Vector3 rotationAxis;
    [SerializeField]
    float rotationRateMultiplier = 1.0f;
    [SerializeField]
    AnimationCurve rotationCurve;
    [SerializeField]
    private bool pingPong = false;

    [SerializeField]
    Transform platform;
    private PlatformParenter platformParenter;

    private bool positiveRotation = true;
    private float curvePos = 0.0f;

    private void Start()
    {
        platformParenter = platform.GetComponent<PlatformParenter>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (platformParenter.player == null)
            Rotate();
        else
        {
            ThirdPersonMovement.controller.enabled = false;
            Rotate();
            ThirdPersonMovement.controller.enabled = true;
        }
    }

    private void Rotate()
    {
        // Rotation
        Vector3 currentRotation = rotationAxis * rotationCurve.Evaluate(GetCurvePosition());
        platform.rotation = Quaternion.Euler(currentRotation);
    }

    private float GetCurvePosition()
    {
        if (pingPong)
        {
            if (positiveRotation)
                curvePos += Time.fixedDeltaTime * rotationRateMultiplier;
            else
                curvePos -= Time.fixedDeltaTime * rotationRateMultiplier;

            if (curvePos > 1.0f)
            {
                curvePos = 1.0f;
                positiveRotation = false;
            }
            else if (curvePos < 0.0f)
            {
                curvePos = 0.0f;
                positiveRotation = true;
            }
        }
        else
        {
            curvePos += Time.fixedDeltaTime * rotationRateMultiplier;

            if (curvePos > 1.0f)
            {
                curvePos -= 1.0f;
            }
        }

        return curvePos;
    }
}
