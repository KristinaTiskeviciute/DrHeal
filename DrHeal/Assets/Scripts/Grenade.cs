using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {
    public float radius = 6f;
    public GameObject hit;
    public Collider2D[] colliders;
    public int damage = 3;
    public float fireRate = 2f;
    public float Damage = 10;
    public LayerMask notToHit;
    public GameObject bullet;
    public float bulletForce;
    public Quaternion rotationB;
    public Quaternion rotationA;
    public int grenadeCount =6;
    public GameObject empty;
    public GameObject oneThird;
    public GameObject twoThird;
    public GameObject full;

    public int pooledAmount = 20;
    List<GameObject> bullets;


    float timeToFire = 0.5f;
    public Transform firePoint;
    


    // Use this for initialization
    void Awake()
    {
        if (firePoint == null)
        {
            Debug.LogError("No firepoint, STAHP");
        }
    }

    void Start()
    {
        empty = GameObject.Find("BottleEmpty");
        oneThird = GameObject.Find("BottleOneThird");
        twoThird = GameObject.Find("BottleTwoThird");
        full = GameObject.Find("BottleFull");
        bullets = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(bullet);
            obj.SetActive(false);
            bullets.Add(obj);
        }
        rotationB.eulerAngles = new Vector3(0, 0, 162);
        rotationA.eulerAngles = new Vector3(0, 0, -18);
    }
    // Update is called once per frame
    void Update()
    {
        timeToFire -= Time.deltaTime;
        if (fireRate == 0)
        {
            if (Input.GetKeyDown("l")&& grenadeCount >=6)
            {
                grenadeCount -= 6;
                Shoot();
            }
        }
        else
        {
            if (Input.GetKeyDown("l") && timeToFire <= 0 && grenadeCount>=6)
            {
                grenadeCount -= 6;
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
        if (GameObject.Find("hero").GetComponent<SimplePlatformerController>().facingRight)
        {
            GameObject bulletInstance = Instantiate(bullet, transform.position, rotationA);
            Rigidbody2D tempRB;
            bulletInstance.SetActive(true);
            tempRB = bulletInstance.GetComponent<Rigidbody2D>();
            tempRB.AddForce(Vector2.one * bulletForce);
        }
        if (!GameObject.Find("hero").GetComponent<SimplePlatformerController>().facingRight)
        {
            GameObject bulletInstance = Instantiate(bullet, transform.position, rotationB);
            Rigidbody2D tempRB;
            bulletInstance.SetActive(true);

            tempRB = bulletInstance.GetComponent<Rigidbody2D>();
            tempRB.AddForce(new Vector2(-1,1) * bulletForce);
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

    void grenadeReady()
    {
        if (grenadeCount < 23)
        {
            grenadeCount += 1;
        }
        if (grenadeCount > 6 && grenadeCount < 12)
        {
            oneThird.SetActive(true);
            empty.SetActive(false);
            twoThird.SetActive(false);
            full.SetActive(false);
        }
        if (grenadeCount < 6)
        {
            oneThird.SetActive(false);
            empty.SetActive(true);
            twoThird.SetActive(false);
            full.SetActive(false);

        }
        if (grenadeCount > 12 && grenadeCount < 18)
        {
            oneThird.SetActive(false);
            empty.SetActive(false);
            twoThird.SetActive(true);
            full.SetActive(false);

        }
        if (grenadeCount > 18)
        {
            oneThird.SetActive(false);
            empty.SetActive(false);
            twoThird.SetActive(false);
            full.SetActive(true);

        }
    }
}
