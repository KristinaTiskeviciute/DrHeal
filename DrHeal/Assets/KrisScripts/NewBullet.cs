using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBullet : MonoBehaviour {

    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector2 direction;


    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        
    }
    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }
    // Update is called once per frame
    void Update () {
        myRigidbody.velocity = direction * speed;

    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
            Destroy(gameObject);
        
    }
}
