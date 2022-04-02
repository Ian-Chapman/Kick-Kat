using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParenter : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            player = other.gameObject;
            other.transform.SetParent(transform, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            player = null;
            other.transform.SetParent(null, true);
        }
    }
}
