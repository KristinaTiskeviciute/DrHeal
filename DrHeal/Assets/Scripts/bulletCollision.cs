using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCollision : MonoBehaviour {
    private void Start()
    {
      
    }

    void OnTriggerEnter2D(Collider2D col)
    {
     
        if(col.gameObject.tag == "Enemy" && tag != "Enemy" || col.gameObject.tag == "Player" && tag != "Bullet")
        {
            col.gameObject.SendMessage("ApplyDamage", 2);
            Destroy(gameObject);
        }

    }
}
