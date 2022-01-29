using UnityEngine;
using System.Collections;

public class PlayCeilingFanAnimation : MonoBehaviour
{
    public bool TurnFanOn = false;
	// Use this for initialization
	void Start ()
	{
	    if (TurnFanOn)
	    {
	        GetComponent<Animation>().Play("CeilingFanRotate");
	    }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
