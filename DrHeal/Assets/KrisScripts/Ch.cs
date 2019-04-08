using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ch : MonoBehaviour {

    public Animator MyAnimator { get; private set; }

    [SerializeField]
    public float movementSpeed;
    public bool facingRight;

    [SerializeField]
    protected int health = 5;
    protected bool hurt = false;

    public bool Attack { get; set; }
    public bool TakingDamage { get; set; }
    
    private SpriteRenderer sr;
    

    public virtual void Start () {
        MyAnimator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

    }
	
	
	void Update () {
        
	}


    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    
    
    private IEnumerator Blink()
    {      
        while (hurt){
            sr.enabled = false;
            yield return new WaitForSeconds(.15f);
            sr.enabled = true;
            yield return new WaitForSeconds(.15f);
        }
    }


    public virtual bool IsDead()
    {
            return health <= 0;      
    }


    public virtual IEnumerator TakeDamage( int damage)
    {         
            health -= damage;
            if (!IsDead())
            {
                MyAnimator.SetTrigger("damage");
            }
            else
            {
                MyAnimator.SetTrigger("die");
            }
        StartCoroutine(Blink());
            yield return new WaitForSeconds(0.1f);
    }
    
}
