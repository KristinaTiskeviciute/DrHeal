using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cakeslice
{
    public class Toggle : MonoBehaviour
    {
        public bool invincible = false;
        public GameObject body;
       
        // Use this for initialization
        void Start()
        {
            body.GetComponent<Outline>().enabled = false;
        }
        private void Awake()
        {
            body.GetComponent<Outline>().enabled = false; 
        }
        // Update is called once per frame
        void Update()
        {

        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (!invincible)
            {
                if (collision.gameObject.tag == "Enemy")
                {
                    body.GetComponent<Outline>().enabled = true;
                    Invoke("resetInvulnerability", 1);
                }
            }
        }

        void resetInvulnerability()
        {
            invincible = false;
            body.GetComponent<Outline>().enabled = false;
        }
    }
}