using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Ch {

    private IEnemyState currentState;

    [SerializeField]
    protected int bulletDamage = 1;
    [SerializeField]
    protected int grenadeDamage = 2;
    [SerializeField]
    protected float immortalityTime = 1f;

    public GameObject Target { get; set; } 
    private bool moveBlock = false;


    public override void Start () {
        base.Start();       
        ChangeState(new IdleState());
    }
	
	
	public virtual void Update () {
        if (!IsDead())
        {
            if (!TakingDamage)
            {
                currentState.Execute();
            }
            LookAtTarget();
        }
 
        
	}


    public void ChangeState(IEnemyState newState)
    {
        if( currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }


    public void Move(float speedMultiplier)
    {
        if (!Attack&&!moveBlock)
        {
            MyAnimator.SetFloat("speed", 1);
            Vector3 s = GetDirection() * movementSpeed*speedMultiplier * Time.deltaTime;
            transform.Translate(s);
        }  
    }


    public  Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            StartCoroutine(TakeDamage(bulletDamage));
        }

        if (other.tag == "Grenade")
        {
            StartCoroutine(TakeDamage(grenadeDamage));
        }
        
        currentState.OnTriggerEnter(other);
    }

    
    private void LookAtTarget()
    {
        if (Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;
            if (xDir <0 && facingRight || xDir >0 && !facingRight)
            {
                ChangeDirection();
            }
        }
    }

    public IEnumerator die()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    public override IEnumerator TakeDamage(int damage)
    {
            moveBlock = true;
            hurt = true;
            yield return base.TakeDamage(damage);
            yield return new WaitForSeconds(immortalityTime);
            hurt = false;
            moveBlock = false;
            if (IsDead())
        {
            StartCoroutine(die());
        }
    }
}
