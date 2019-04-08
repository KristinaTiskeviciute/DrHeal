using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour {


    public CutSceneScript scene;
    public string images;
    public string sceneName;
	// Use this for initialization
	void Start () {


        scene.play(images, 5.0f);
        GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
        
        if (sceneName == "Opening" && !GetComponent<AudioSource>().isPlaying && !scene.isPlaying)
        {


            
            GetComponent<ChangeScenes>().goToNext();

        }
		
	}
}
