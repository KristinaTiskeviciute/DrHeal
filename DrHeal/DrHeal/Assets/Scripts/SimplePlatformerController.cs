using UnityEngine;
using System.Collections;
using Spine.Unity;
using UnityEngine.UI;


public class SimplePlatformerController : MonoBehaviour
{

    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    [SerializeField]
    private float fillAmount;
    [SerializeField]
    private Image content;
    //public bool jump = false;
    //public float jumpForce;
    public bool grounded;
    //public float moveForce;
    //public float maxSpeed;
    public float crouchHeight;
    public float crouchOffset;
    public float standHeight;
    public float standOffset;
    public bool bIsCrouched;
    public bool invincibilityFrame = false;

    public Transform groundCheck;
    public BoxCollider2D PlayerCollision;
    private Animator anim;
    private Rigidbody2D rb2d;
    public float health = 3;
    public GameObject player;
    public GameObject respawnPoint;
    public bool invincible = false;
    private SkeletonAnimation skeletonAnimation;


    // Use this for initialization
    void Awake()
    {
        PlayerCollision = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        respawnPoint = GameObject.Find("Respawn Point");
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        //if (Input.GetButtonDown("Jump") && grounded)
        //{
        //    jump = true;
       // }

        if (Input.GetButton("Crouch") && grounded)
        {
            Crouch();
        }
        if (bIsCrouched)
        {
            if (Input.GetButtonUp("Crouch") || !grounded)
                StandUp();
                skeletonAnimation.AnimationName = "Kris2";
        }
        HandleBar();
    }

    void ApplyDamage(int hit)
    {
        if (!invincible)
        {
            health--;
            Invoke("resetInvulnerability", 1);
        }
        if (health <= 0)
        {
            health = 3;
            player.transform.position = respawnPoint.transform.position;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log(tag + " + " + collision.transform.tag);
        if (collision.gameObject.CompareTag("fish"))
        {
            collision.gameObject.SetActive(false);
        }
        else if (!invincible)
        {
            Debug.Log("Got Hit");
            if (collision.gameObject.tag == "Enemy")
            {
                invincible = true;
                health -= 1;
                Invoke("resetInvulnerability", 2);
 
                //anim.SetTrigger("IsHit");

            }
        }
        if (health == 0)
        {
            health = 3;
            player.transform.position = respawnPoint.transform.position;
            //anim.SetTrigger("Death")
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

       //// anim.SetFloat("Speed", Mathf.Abs(h));

       // if (h * rb2d.velocity.x < maxSpeed)
       //     rb2d.AddForce(Vector2.right * h * moveForce);

       // if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
       //     rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

       //if (jump)
       // {
       //     //anim.SetTrigger("Jump");
       //     rb2d.AddForce(new Vector2(moveForce*rb2d.velocity.x, moveForce*jumpForce));
            
       //     jump = false;
       // }

    }
    void resetInvulnerability()
    {
        invincible = false;
    }
    void Crouch()
    {
        bIsCrouched = true;
        PlayerCollision.size = new Vector2(PlayerCollision.size.x, crouchHeight);
        PlayerCollision.offset = new Vector2(0, crouchOffset);
        skeletonAnimation.AnimationName = "Duck";
        //anim.SetTrigger("Crouch");
    }

    void StandUp()
    {
        bIsCrouched = false;
        PlayerCollision.size = new Vector2(PlayerCollision.size.x, standHeight);
        PlayerCollision.offset = new Vector2(0, standOffset);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void HandleBar()
    {
        content.fillAmount = Map(health, 0, 3, 0, 1);
    }
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}