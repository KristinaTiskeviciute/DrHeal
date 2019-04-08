using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawphysirAI : MonoBehaviour
{
    [HideInInspector]
    public int AIFacingRight = 1;
    [HideInInspector]
    public bool jump = false;
    [HideInInspector]
    public int health = 8;
    public bool invincibility = true;
    public float disableTime;
    public float moveForce = 365f;
    public float maxSpeed = 15f;
    public float jumpForce = 3000f;
    public float nextCommandIn = 1.4f;
    public float horizforce = -1.5f;
    public float cooldown = 0f;
    public float coolRefresh = 0.5f;
    public float spitForce = 2500;
    public int distance;
    public bool hunting = false;
    public bool ranged = false, melee = false;
    public Transform groundCheck;
    public Transform findPlayer;
    public LayerMask notToHit;

    public Quaternion rotationB;
    public Quaternion rotationA;
    public GameObject spit;
    public int pooledAmount = 5;
    List<GameObject> bullets;

    private bool grounded = false;
    private Animator anim;
    private Rigidbody2D rb2d;
    public Transform firePoint;


    // Use this for initialization
    void Awake()
    {

        if (firePoint == null)
        {

        }

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

        findPlayer = GameObject.Find("hero").transform;
        

        bullets = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(spit);
            obj.SetActive(false);
            bullets.Add(obj);
        }

        rotationB.eulerAngles = new Vector3(0, 0, 180);
        rotationA.eulerAngles = new Vector3(0, 0, -26);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(cooldown);
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        distance = DistSq(findPlayer.position, groundCheck.position);
        if ((FacePlayer(findPlayer.position.x) && distance < 1000))
        {
            cooldown -= Time.deltaTime;
            //Debug.Log(distance);
            if (distance <= 900 && cooldown <= 0)
            {
               
                if (distance > 200)
                {
                   
                    ranged = true;
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
         
            nextCommandIn = Random.value * 2;
            if (nextCommandIn > 1.6f && grounded)
            {
                nextCommandIn = Random.value * 2;
                jump = true;
            }
            horizforce = Random.value * maxSpeed * 2 - maxSpeed;
        }
        

    }

    void FixedUpdate()
    {

        float h = horizforce;

        if (!invincibility)
        {
            if (disableTime <= 0)
            {
                invincibility = !invincibility;
            }
            disableTime -= Time.deltaTime;

        }
        else
        {
            //anim.SetFloat("Speed", Mathf.Abs(h));

            if (h * rb2d.velocity.x < maxSpeed)
                rb2d.AddForce(Vector2.right * h * moveForce);

            if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
                rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

            if (h > 0 && AIFacingRight < 0)
                Flip();
            else if (h < 0 && AIFacingRight > 0)
                Flip();

            if (jump)
            {
                //anim.SetTrigger("Jump");
                rb2d.AddForce(new Vector2(jumpForce / 4, jumpForce));
                jump = false;
            }

            if (melee)
            {
                melee = false;
            }

            if (ranged)
            {
               
                Shoot();
                ranged = false;
            }
        }

        
        nextCommandIn -= Time.deltaTime;
    }

    void ApplyDamage(int hit)
    {
        if (!invincibility)
        {
            health -= hit;
           
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
        else if(hit >= 2)
        {
            invincibility = false;
            disableTime = 3f;
        }
    }

    int DistSq(Vector3 play, Vector3 AI)
    {
        return (int)(Mathf.Pow((play.x - AI.x), 2) + Mathf.Pow((play.y - AI.y), 2));
    }

    bool FacePlayer(float playX)
    {
        return (playX - groundCheck.position.x) * AIFacingRight > 0;
    }

    void Shoot()
    {
        //Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        //Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        //RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, notToHit);
        //Debug.DrawLine(firePointPosition, hit.point, Color.green); 
        

        if (AIFacingRight > 0)
        {
            
            GameObject bulletInstance = Instantiate(spit, firePoint.transform.position, rotationA);
            Rigidbody2D tempRB;
            tempRB = bulletInstance.GetComponent<Rigidbody2D>();
            tempRB.AddForce(Vector2.right * spitForce);
        }
        if (AIFacingRight < 0)
        {
          
            GameObject bulletInstance = Instantiate(spit, firePoint.transform.position, rotationB);
            Rigidbody2D tempRB;
            tempRB = bulletInstance.GetComponent<Rigidbody2D>();
            tempRB.AddForce(-Vector2.right * spitForce);
        }
        /*for (int i = 0; i< bullets.Count; i++)
            if (!bullets[i].activeInHierarchy)
            {

                Rigidbody2D tempRB;
                tempRB = bullets[i].GetComponent<Rigidbody2D>();
                tempRB.AddForce(Vector2.right * bulletForce);

                // bullets[i].transform.position = transform.position;
                //bullets[i].transform.rotation = transform.rotation;
                bullets[i].SetActive(true);
                 break;

            }
    }*/
    }

    void Flip()
    {
        AIFacingRight *= -1;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
