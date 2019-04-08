using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneScript : MonoBehaviour {

    List<Texture> frames;
    int currentFrame;
    float timePerFrame;
    private bool playing;


    private void Awake()
    {
        frames = new List<Texture>();
        currentFrame = 0;
        timePerFrame = 0;
        playing = false;
    }

    public void play(string sceneName, float timePerFrame)
    {

        playing = true;

        frames.Clear();
        currentFrame = 0;
        this.timePerFrame = timePerFrame;
        load(sceneName);
        StartCoroutine(play());
    }

    public bool isPlaying
    {
        get
        {
            return playing;
        }
    }

    private void load(string sceneName)
    {

        Object[] objects = Resources.LoadAll(sceneName, typeof(Texture));


        foreach (Object obj in objects)
        {

            frames.Add(obj as Texture);
            Debug.Log(frames[frames.Count - 1].name);

        }
        Debug.Log(frames.Count);
    } 
    private IEnumerator play()
    {
        while (currentFrame < frames.Count)
        {
            GetComponent<Renderer>().material.mainTexture = frames[currentFrame];
            currentFrame++;


            yield return new WaitForSeconds(timePerFrame);
        }
        playing = false;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
