using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : Enemy {

    
    [SerializeField]
    protected GameObject bulletPrefab;

    // Use this for initialization
    public override void Start () {
        base.Start();
    }
	
	// Update is called once per frame
	public override void Update () {
        base.Update();
	}


   public void Shoot(int value, float charSpeedx)
    {

        if (!IsDead())
        {
            if (facingRight)
            {
                GameObject tmp = (GameObject)Instantiate(bulletPrefab, transform.position + .2f * new Vector3(charSpeedx, 0, 0), Quaternion.Euler(new Vector3(0, 0, -180)));
                tmp.GetComponent<NewBullet>().Initialize(Vector2.right);
            }
            else
            {
                GameObject tmp = (GameObject)Instantiate(bulletPrefab, transform.position + .2f * new Vector3(charSpeedx, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
                tmp.GetComponent<NewBullet>().Initialize(Vector2.left);
            }
        }

    }
}
