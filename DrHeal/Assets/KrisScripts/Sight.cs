using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour {

    [SerializeField]
    private Enemy enemy;
    private void Start()
    {
        
        IgnoreColl(GameObject.FindGameObjectsWithTag("Enemy"));
     

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.tag == "Player")
        {
            enemy.Target = other.gameObject;          
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enemy.Target = null;
        }
    }
    private void IgnoreColl(GameObject[]objectsToIgnore)
    {
        foreach (GameObject go in objectsToIgnore)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), go.GetComponent<Collider2D>());

        }
    }
}
