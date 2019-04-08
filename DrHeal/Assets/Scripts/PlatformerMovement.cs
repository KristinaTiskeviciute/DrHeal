using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformerMovement : MonoBehaviour {
    
    public float moveSpeed = 15;
    public float lerpSpeed = 2; // How fast we change speed

    private Rigidbody2D myRigidbody;

    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>(); // Get this so we can modify it
	}

    private void UpdateSpeed()
    {
        float inSpeed = Input.GetAxis("Horizontal");

        myRigidbody.velocity = new Vector3(
            Mathf.Lerp(myRigidbody.velocity.x, moveSpeed * inSpeed, lerpSpeed), // Get our new horizontal speed
            myRigidbody.velocity.y // don't change vertical speed
            );
    }
	
	// Update is called once per frame
	void Update () {
        UpdateSpeed();
	}
}
