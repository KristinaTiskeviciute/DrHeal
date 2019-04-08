using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("heeey");
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<Rigidbody2D>().gravityScale = 10.0f;
        }
    }
}
