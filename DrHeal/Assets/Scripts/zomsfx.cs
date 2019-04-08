using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class zomsfx : MonoBehaviour {

    private AudioSource audio1;

    private bool playLeft = false;

    public List<AudioClip> leftFeet;
    public List<AudioClip> rightFeet;

    public float timeBetweenSteps = 1;
    private float nextStepTime;
   

	// Use this for initialization
	void Start () {
        audio1 = GetComponent<AudioSource>();
        nextStepTime = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
        if ( Time.time > nextStepTime)
        {
            nextStepTime = Time.time + timeBetweenSteps;
            PlayFootstep();
        } 
		
	}

    private void PlayFootstep(){
        if (playLeft)
        {
            playLeft = false;
            audio1.clip = leftFeet[Random.Range(0, leftFeet.Count - 1)];

        }
        else
        {
            playLeft = true;
            audio1.clip = rightFeet[Random.Range(0, rightFeet.Count - 1)];
        }

        audio1.Play();
    }
}
