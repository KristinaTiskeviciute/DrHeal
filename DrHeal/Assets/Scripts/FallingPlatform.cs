﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
	
	// Update is called once per frame
	void Update () {
        
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<Rigidbody2D>().bodyType= RigidbodyType2D.Dynamic;
        }
    }
}
