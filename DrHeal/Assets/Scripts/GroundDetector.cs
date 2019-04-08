/**using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GroundDetector : MonoBehaviour {

    public float leniencyTime = 0.1f;
    private float timeToTurnOff;

    public bool touch;

    void Start()
    {
        touch = false;
        timeToTurnOff = Time.time + leniencyTime;
    }

    void Update()
    {
        if(Time.time > timeToTurnOff)
        {
            touch = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        touch = true;
        timeToTurnOff = Time.time + leniencyTime;
    }

    public bool IsTouching()
    {
        return touch;
    }
}*/
