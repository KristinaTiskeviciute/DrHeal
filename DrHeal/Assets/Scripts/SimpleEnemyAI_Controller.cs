using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyAI_Controller : MonoBehaviour {


    [HideInInspector]
    public int AIFacingRight = 1;
    [HideInInspector]
    public bool jump = false;
    public int health = 4;
    public bool invincibilityFrame = false;
    public float moveForce;
    public float maxSpeed;
    public float jumpForce;
    public float nextCommandIn;
    public float horizforce;
    public float cooldown;
    public float coolRefresh;
    public float attackTime;
    public int distance;
    public bool hunting = false;
    public bool ranged = false, melee = false;
    public Transform groundCheck;
    public Transform findPlayer;
    public bool grenadeHit = false;

    [SerializeField]
    private Transform LeftEdge;
    [SerializeField]
    private Transform RightEdge;

    private bool grounded = false;
    private Animator anim;
    private Rigidbody2D rb2d;

    void Start()
    {
        findPlayer = GameObject.Find("hero").transform;
    }

    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        distance = DistSq(findPlayer.position, groundCheck.position);
        //Debug.Log(distance);
        if ( (FacePlayer(findPlayer.position.x, groundCheck.position.x) && distance < 1000))
        {
            cooldown -= Time.deltaTime;
            if (distance < 800 && cooldown <= 0)
            {
                attackTime -= Time.deltaTime;
                if (distance > 400)
                {
                    ranged = true;
                    attackTime = 0.14f;
                }
                else
                {
                    melee = true;
                }
                cooldown = coolRefresh;
            }
        }
        else if (nextCommandIn <= 0.0f)
        {
            attackTime = 0f;
            nextCommandIn = Random.value * 2;
            if (nextCommandIn > 1.6f && grounded)
            {
                nextCommandIn = Random.value * 2;
                jump = true;
            }
            horizforce = Random.value * maxSpeed*2 - maxSpeed;
        }
        
    }

    void FixedUpdate()
    {
        
        float h = horizforce;

        anim.SetFloat("Speed", Mathf.Abs(h));

        //if (h * rb2d.velocity.x < maxSpeed)
        rb2d.AddForce(Vector2.right *moveForce);


        // if (Mathf.Abs(rb2d.velocity.x) > maxSpeed && attackTime <= 0)
        //rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
        //rb2d.MovePosition(rb2d.position + rb2d.velocity* Time.fixedDeltaTime);

        if ( h > 0 && AIFacingRight < 0 )
            Flip();
        else if ( h < 0 && AIFacingRight > 0 )
            Flip();

       // if (jump)
        //{
       //     //anim.SetTrigger("Jump");
       //     rb2d.AddForce(new Vector2(moveForce * rb2d.velocity.x, moveForce * jumpForce));
      //      jump = false;
      //  }

        if (melee)
        {
            melee = false;
        }

        if (ranged)
        {
            //anim.SetTrigger("Lunge");
            rb2d.AddForce(new Vector2((moveForce+jumpForce/2f) * rb2d.velocity.x, jumpForce/2f));
            ranged = false;
        }

        invincibilityFrame = false;


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

    bool FacePlayer(float playX, float AIx)
    {
        return ( playX - AIx ) * AIFacingRight > 0;
    }

    void Flip()
    {
        AIFacingRight *= -1;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
