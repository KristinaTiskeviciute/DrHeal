using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGrenade : MonoBehaviour {

    [SerializeField]
    private float speed;
    private Rigidbody2D myRigidbody;
    private Vector2 direction;
    private float charSpeedx;
    private SpriteRenderer sr;
    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myRigidbody.velocity = (direction * speed + new Vector2(charSpeedx,0));
        myRigidbody.velocity = myRigidbody.velocity + Vector2.up * 7;

    }

    public void Initialize(Vector2 direction,float charSpeedx)
    {
        this.direction = direction;
        this.charSpeedx = charSpeedx;
    }

    // Update is called once per frame
    void Update () {
		
	}
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        
        StartCoroutine(delayDestroy());
            
    }
    private IEnumerator delayDestroy()
    {
        yield return new WaitForSeconds(.1f);
        sr.enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);

    }
}
