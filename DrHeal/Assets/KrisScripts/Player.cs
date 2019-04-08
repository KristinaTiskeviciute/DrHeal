using System.Collections;

using UnityEngine;

public class Player : Ch {

    //damage and immortality
    [SerializeField]
    protected int meleeDamage = 1;
    [SerializeField]
    protected int bulletDamage = 1;
    [SerializeField]
    private float immortalityTime = 1f;

    [SerializeField]
    protected GameObject bulletPrefab;
    [SerializeField]
    protected GameObject grenadePrefab;

    private  static Player instance;
    public static Player Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
        
    }


    //jumping
    public Rigidbody2D MyRigidbody { get; set; }
    [SerializeField]
    private float jumpForce;
    private Vector3 offsetCenter; //offset from player transform to circle collider ray origins for checking if on ground
    private Vector3 offsetLeft;
    private Vector3 offsetRight;
    private CircleCollider2D circleCollider;
    private LayerMask layermaskGround;


    //ducking
    [SerializeField]
    private bool ducking = false;
    private bool jumping = false;
    private bool grounded;
    
    private bool expanded = true;
    Vector2 boxSize;
    private float offsetY;
    Vector2 smallBoxSize;
    Vector2 collOffset;
    Vector2 smallCollOffset;
   

    //shooting throwing and cooldowns
    [SerializeField]
    private float shootCooldown;
    [SerializeField]
    private float grenadeCooldown;
    private bool canShoot=true;
    private bool canThrow=true;
    private bool canMove = true;
    private Transform gunTransform;

    

    
    public BoxCollider2D MyBoxCollider { get; set; }
    
    public bool Duck
    {
        get { return ducking; }
        set { ducking = value;}
    }
    

    public bool Jump
    {
        get { return jumping; }
        set { jumping = value; }
    }

    public bool Grounded
    {
        get{ return grounded; }
    }

 
    public override void Start ()
    {
        base.Start();
        ChangeDirection();
        MyRigidbody = GetComponent<Rigidbody2D>();
        MyBoxCollider = GetComponentInChildren<BoxCollider2D>();
        boxSize = MyBoxCollider.size;
        smallBoxSize = (boxSize / 2);
        offsetY = MyBoxCollider.offset.y;
        smallCollOffset = new Vector2(MyBoxCollider.offset.x, -offsetY);
        collOffset=MyBoxCollider.offset;
        gunTransform = transform.Find("gun");
        Debug.Log(gunTransform);

        circleCollider = GetComponent<CircleCollider2D>();
        offsetCenter = circleCollider.offset*transform.localScale.y;
        offsetLeft = new Vector3(offsetCenter.x - (circleCollider.radius*transform.localScale.x), offsetCenter.y, 0);
        offsetRight = new Vector3(offsetCenter.x + (circleCollider.radius*transform.localScale.x), offsetCenter.y, 0);
        layermaskGround = LayerMask.NameToLayer("Ground");
    }
	


    void Update()
    {
        HandleInput();
        grounded = OnGround();
        //Debug.Log("og: "+ Grounded);
        Debug.DrawLine(transform.position + offsetCenter, transform.position + offsetCenter - transform.up, Color.white, 0.1f);
        Debug.DrawLine(transform.position + offsetLeft, transform.position + offsetLeft - transform.up, Color.white, 0.1f);
        Debug.DrawLine(transform.position + offsetRight, transform.position + offsetRight - transform.up, Color.white, 0.1f);
    }


	void FixedUpdate () {
        float horizontal;
        horizontal = Input.GetAxis("Horizontal"); 
        if (!canMove) {
            horizontal = 0;
                };

        HandleMovement(horizontal);
        HandleLayers();
        Flip(horizontal);
          
    }


    private void HandleMovement (float horizontal)
    {
        //landing
       if  ((MyRigidbody.velocity.y < 0 && !grounded)||(grounded && Jump)){
            MyAnimator.SetBool("land", true);
        }
        
        //moving
        MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, MyRigidbody.velocity.y);
        
        //jumping
       if (Jump && !Duck && grounded)
        {

            //construct rays for onGround check
            MyRigidbody.AddForce(new Vector2(0, jumpForce));
            //MyAnimator.SetBool("jump", true);
            Jump = false;
            

        }
        

        //running anim
        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }


    


    private void HandleInput()
    {

        if (Input.GetKeyDown("space") && !ducking && grounded )
        {
            MyAnimator.SetTrigger("jump"); 
        }

        if (Input.GetMouseButtonDown(0) && canShoot && !ducking)
        {
            MyAnimator.SetTrigger("attack");
            Shoot(0, MyRigidbody.velocity.x);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCoroutine(DelayDuck());
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            ducking = false;
            MyAnimator.SetBool("duck", false);
            
            if (!expanded)
            {
                Expand();
            }
        }
        if (Input.GetKeyDown(KeyCode.V) && canThrow && !ducking)
        {
            MyAnimator.SetTrigger("throw");
            canMove = false;
            StartCoroutine(ThrowDelay());


        }

    }


   
    private void HandleLayers()
    {
        if (!grounded)
        {
            MyAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            MyAnimator.SetLayerWeight(1, 0);
        }
    }



    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();
            offsetCenter = new Vector2(circleCollider.offset.x * transform.localScale.x, circleCollider.offset.y * transform.localScale.y);
            offsetLeft = new Vector3(offsetCenter.x - (circleCollider.radius * transform.localScale.x), offsetCenter.y, 0);
            offsetRight = new Vector3(offsetCenter.x + (circleCollider.radius * transform.localScale.x), offsetCenter.y, 0);
        }

    }

    public bool OnGround()
    {
        RaycastHit2D rayCastHitCenter = Physics2D.Raycast(transform.position + offsetCenter, -transform.up, 2, 1 << layermaskGround);
        if (rayCastHitCenter.collider != null)
        {
            return true;
        }
        RaycastHit2D rayCastHitRight = Physics2D.Raycast(transform.position + offsetRight, -transform.up, 2, 1 << layermaskGround);
        if (rayCastHitRight.collider != null)
        {
            return true;
        }
        RaycastHit2D rayCastHitLeft = Physics2D.Raycast(transform.position + offsetLeft, -transform.up, 2, 1 << layermaskGround);
        if (rayCastHitLeft.collider != null)
        {
            return true;
        }

        return false;

    }

    public virtual void ThrowGrenade(int value, float charSpeedx)
    {
        
        if (!IsDead())
        {
            if (facingRight)
            {
                GameObject tmp = (GameObject)Instantiate(grenadePrefab, transform.position + .2f * new Vector3(charSpeedx, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
                tmp.GetComponent<NewGrenade>().Initialize(Vector2.right, charSpeedx);
            }
            else
            {
                GameObject tmp = (GameObject)Instantiate(grenadePrefab, transform.position + .2f * new Vector3(charSpeedx, 0, 0), Quaternion.Euler(new Vector3(0, -180, 0)));
                tmp.GetComponent<NewGrenade>().Initialize(Vector2.left, charSpeedx);
            }
        }
        StartCoroutine(GrenadeCooldown());
    }


    public virtual void Shoot(int value, float charSpeedx)
    {

        if (!IsDead())
        {
            if (facingRight)
            {
                GameObject tmp = (GameObject)Instantiate(bulletPrefab, transform.position + .2f * new Vector3(charSpeedx, 0, 0), Quaternion.Euler(new Vector3(0, 0, -180)));
                tmp.GetComponent<NewBullet>().Initialize(gunTransform.right);
            }
            else
            {
                GameObject tmp = (GameObject)Instantiate(bulletPrefab, transform.position + .2f * new Vector3(charSpeedx, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
                tmp.GetComponent<NewBullet>().Initialize(-gunTransform.right);
            }
        }
        StartCoroutine(ShootCooldown());
    }
    
 
    private IEnumerator DelayDuck()
    {   
        MyAnimator.SetBool("duck", true);
        yield return new WaitForSeconds(0.2f);
        if (expanded && Input.GetKey(KeyCode.LeftControl))
        {
            ducking = true;
            Contract();
        }
    }


    


    private IEnumerator ThrowDelay()
    {
        yield return new WaitForSeconds(0.2f);
        ThrowGrenade(0, MyRigidbody.velocity.x);
    }


    private IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }


    private IEnumerator GrenadeCooldown()
    {
        canThrow = false;
        yield return new WaitForSeconds(.2f);
        canMove = true;
        yield return new WaitForSeconds(grenadeCooldown-.2f);
        canThrow = true;

    }


  

    private void Expand()
    {
        MyBoxCollider.offset = collOffset;
        MyBoxCollider.size = boxSize;
        expanded = true;
    }


    private void Contract()
    {
        MyBoxCollider.size = smallBoxSize;
        MyBoxCollider.offset = smallCollOffset;
        expanded = false;
    }

    



    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyBullet" && !IsDead())
        {
            StartCoroutine(TakeDamage(bulletDamage));
        }
        if (other.tag == "MeleeColl" && !IsDead())
        {   
            if (other.GetComponent<Transform>().position.x - GetComponent<Transform>().position.x > 0)
            {
                StartCoroutine(KnockBack(true));
            }
            else
            {
                StartCoroutine(KnockBack(false));
            }
            StartCoroutine(TakeDamage(meleeDamage));  
        }
    }


    public override IEnumerator TakeDamage(int damage)
    {
        if (!hurt)
        {
            hurt = true;

            yield return base.TakeDamage(damage);
            yield return new WaitForSeconds(immortalityTime);
            hurt = false;
        }
        
    }
    

    private IEnumerator KnockBack(bool knockRight)
    {
        canMove = false;
        for (int i = 1; i < 15; ++i)
        {
            if (knockRight)
            {
                MyRigidbody.AddForce(new Vector2(-10000 / i, MyRigidbody.velocity.y));
            }
            
            else{
                MyRigidbody.AddForce(new Vector2(10000 / i, MyRigidbody.velocity.y));
            }
            yield return new WaitForSeconds(.03f); }
        yield return new WaitForSeconds(.3f);
        canMove = true;
    }


}


