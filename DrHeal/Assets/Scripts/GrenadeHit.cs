using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeHit : MonoBehaviour {
    public float radius = 6f;
    public GameObject hit;
    public Collider2D[] colliders;
    public int damage = 3;
    private float timeToDie;
    private float explodeTime = .7f;
    public Animator anim;
    // Use this for initialization
    void Awake () {
        hit = gameObject;
        timeToDie = 0;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        if (timeToDie > 0){

            timeToDie -= Time.deltaTime;
        }
        else if (timeToDie < 0)
        {
            //If based on own object, cannot dsetroy.
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
		
	}
    void OnCollisionEnter2D(Collision2D col)
    {
        anim.enabled = true;
        Debug.Log("yoyo");

        Vector3 explosionPos = transform.position;
        colliders = Physics2D.OverlapCircleAll(explosionPos, radius);
        foreach (Collider2D hit in colliders)
        {
            if (hit.gameObject.tag == "Enemy")
            {
                Debug.Log("Hit");
                hit.gameObject.GetComponent<SimpleEnemyAI_Controller>().SendMessage("ApplyDamage", 2);
                

            }
        }
        timeToDie = explodeTime;
        
    }
        
}
