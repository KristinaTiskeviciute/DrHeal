using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeBulletIncrement : MonoBehaviour {
    public Grenade arm;
    public GameObject hero;

    private void Start()
    {
        hero = GameObject.Find("hero");
        arm = hero.GetComponentInChildren<Grenade>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(tag + " + " + col.tag);
        if (col.gameObject.tag == "Enemy" && tag != "Enemy" || col.gameObject.tag == "Player" && tag != "Bullet")
        {
            arm.gameObject.SendMessage("grenadeReady");
        }

    }
}
