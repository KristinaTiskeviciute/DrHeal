using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAI : MonoBehaviour {

    public Vector2 facing;
    public bool swoop = false;
    public bool invincibilityFrame = false;
    public int health = 2;
    public float maxSpeed = 18;
    public float moveForce = 500f;
    public float nextCommandIn = 1.2f;
    public int distance;
    public Vector2 force;
    public float cooldown = 1.2f;
    public Transform me;
    public Transform you;

    private Animator anim;
    private Rigidbody2D rb2d;

    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
        rb2d.velocity = new Vector2(Random.value * maxSpeed * 2 - maxSpeed, Random.value * maxSpeed - (maxSpeed / 2));
        
        facing.x = -1;
        facing.y = -1;
	}

    // Update is called once per frame
    void Update()
    {
        //distance = DistSq(you.position, me.position);
        //if ( distance < 3000 )
        //{
        //    cooldown -= Time.deltaTime;
        //    if ( distance < 60 && cooldown <= 0 )
        //    {
        //        swoop = true;
        //    }
            if (nextCommandIn <= 0)
            {
                nextCommandIn = Random.value * 2;
                rb2d.velocity = new Vector2(Random.value * maxSpeed * 2 - maxSpeed, Random.value * maxSpeed - (maxSpeed / 2));
            }
       // }
    }

    void FixedUpdate()
    {
        if (rb2d.velocity.x > 0 && facing.x < 0)
            Flip();
        else if (rb2d.velocity.x < 0 && facing.x > 0)
            Flip();
        nextCommandIn -= Time.deltaTime;
    }

    void ApplyDamage(int hit)
    {
        if (!invincibilityFrame)
        {
            health -= hit;
            Debug.Log(health);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
        invincibilityFrame = !invincibilityFrame;
    }

    int DistSq(Vector3 play, Vector3 AI)
    {
        return (int)(Mathf.Pow((play.x - AI.x), 2) + Mathf.Pow((play.y - AI.y), 2));
    }

    bool FacePlayer(Vector3 play, Vector3 AI)
    {
        if ((play.x - AI.x) * facing.x < 0)
        {
            facing.x = -facing.x;
        } 
        if ((play.y - AI.y) * facing.y < 0)
        {
            facing.y = -facing.y;
        }
        return true;
    }

    void Flip()
    {
        facing.x *= -1;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
