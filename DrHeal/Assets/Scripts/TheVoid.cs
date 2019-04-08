using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheVoid : MonoBehaviour {

    public GameObject player;
    public Transform respawnPoint;


    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("hero");
    }





     void OnTriggerEnter2D(Collider2D collision){

            if (collision.gameObject.tag == "Player") {
                player.transform.position = respawnPoint.transform.position;

            }
        }

    }

