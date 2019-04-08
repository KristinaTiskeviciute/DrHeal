using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float fireRate = 0.5f;
    public float Damage = 10;
    public LayerMask notToHit;
    public GameObject bullet;
    public float bulletForce;
    public Quaternion rotationB;
    public Quaternion rotationA;

    public int pooledAmount = 5;
    List<GameObject> bullets;


    float timeToFire = 0.5f;
    Transform firePoint;



    // Use this for initialization
    void Awake()
    {
        firePoint = transform.Find( "FirePoint" );
        if ( firePoint == null )
        {
            Debug.LogError( "No firepoint, STAHP" );
        }
    }

    void Start()
    {
        bullets = new List<GameObject>();
        for ( int i = 0; i < pooledAmount; i++ )
        {
            GameObject obj = Instantiate( bullet );
            obj.SetActive( false );
            bullets.Add( obj );
        }
        rotationB.eulerAngles = new Vector3( 0, 0, 162 );
        rotationA.eulerAngles = new Vector3(0, 0, -18);
    }
    // Update is called once per frame
    void Update()
    {
        timeToFire -= Time.deltaTime;
        if (fireRate == 0)
        {

            if (Input.GetKeyDown("k"))
            {
                Shoot();


            }
        }
        else
        {

            if ( Input.GetKeyDown("k") && timeToFire <= 0 )
            {
                timeToFire = fireRate;


                Shoot();
            }
        }

    }

    void Shoot()
    {
        //Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        //Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        //RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, notToHit);
        //Debug.DrawLine(firePointPosition, hit.point, Color.green);   




        GameObject h = GameObject.Find("hero");


        if (h == null)
        {

            return;
        }
        else
        {
            Debug.Log(h.name);

            SimplePlatformerController spc = h.GetComponent<SimplePlatformerController>();

            if (spc == null)
            {
   
                return;
            }
        }

        if (Input.GetKeyDown("w"))
        {
            GameObject bulletInstance = Instantiate(bullet, transform.position, rotationA);
            Rigidbody2D tempRB;
            tempRB = bulletInstance.GetComponent<Rigidbody2D>();
            tempRB.AddForce(Vector2.up * bulletForce);
  

        }
        else if (GameObject.Find("hero").GetComponent<SimplePlatformerController>().facingRight)
        {
            GameObject bulletInstance = Instantiate(bullet, transform.position, rotationA);
            Rigidbody2D tempRB;
            tempRB = bulletInstance.GetComponent<Rigidbody2D>();
            tempRB.AddForce(Vector2.right * bulletForce);


        }
        else if (!GameObject.Find("hero").GetComponent<SimplePlatformerController>().facingRight)
        {
     

            GameObject bulletInstance = Instantiate(bullet, transform.position, rotationB);
            Rigidbody2D tempRB;
            tempRB = bulletInstance.GetComponent<Rigidbody2D>();
            tempRB.AddForce(-Vector2.right * bulletForce);
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
}


