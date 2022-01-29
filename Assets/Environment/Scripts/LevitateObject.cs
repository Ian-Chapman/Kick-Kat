using UnityEngine;
using System.Collections;


public class LevitateObject : MonoBehaviour
{
    
   
    
     float StartXTransform;
     float StartYTransform;
     float StartZTransform;

   

    //the speed of the movement.
    public float MovementSpeed = 0.5f;

    //How far the object will move on the positive and negative of each axis.
    public float MaxXAxisMovement = 0.0f;
    
    public float MaxYAxisMovement = 0.0f;
   
    public float MaxZAxisMovement = 0.0f;
 

	// Use this for initialization
	void Start ()
	{
        //Get the x,y,x positions at start
        StartXTransform = transform.position.x;
	    StartYTransform = transform.position.y;
        StartZTransform = transform.position.z;
        
	}
	
	// Update is called once per frame
	void Update () 
    {

        MoveXYZAxis();
	}

    public void MoveXYZAxis()
    {
        
        SetTransformXYZ(MaxXAxisMovement, MaxYAxisMovement, MaxZAxisMovement);
    
    }



     void SetTransformXYZ(float x, float y, float z)
     {
         transform.position = new Vector3((StartXTransform + x * Mathf.Sin(MovementSpeed * Time.time)), (StartYTransform + y * Mathf.Sin(MovementSpeed * Time.time)), (StartZTransform + z * Mathf.Sin(MovementSpeed * Time.time)));
     }


}
