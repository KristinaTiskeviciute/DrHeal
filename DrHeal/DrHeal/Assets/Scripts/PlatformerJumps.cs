/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformerJumps : MonoBehaviour {

    public float jumpSpeed = 5;
    public int airJumpsAllowed = 1;// 2 allowed for double jumping
    public int jumpsTaken;

    public GroundDetector groundDetector;

    private Rigidbody2D myRigidbody;

    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>(); // Get this so we can modify it
        jumpsTaken = 0;
    }
	
	// Update is called once per frame
	void Update () {

        // Jump if you can
		if( Input.GetButtonDown("Jump") && ( jumpsTaken < airJumpsAllowed || groundDetector.IsTouching() )  )
       {
            jumpsTaken++;
            myRigidbody.velocity = new Vector3(
                myRigidbody.velocity.y,
                jumpSpeed
                );
        }

        // If we're on ground, reset jump stuff
        if( groundDetector.IsTouching() )
        {
            jumpsTaken = 0;
        }
	}


}*/
