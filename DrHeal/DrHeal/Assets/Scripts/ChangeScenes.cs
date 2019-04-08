using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour {


	[SerializeField]private string loadLevel;
	void OnTriggerEnter2D(Collider2D other){
	

		if(other.CompareTag("Player")){
            float fadeTime = GameObject.Find("FadeObj").GetComponent<Fading>().BeginFade(1);
           // yield return new WaitForSeconds(fadeTime);
            SceneManager.LoadScene(loadLevel,LoadSceneMode.Single);
            GameObject.Find("FadeObj").GetComponent<Fading>().BeginFade(-1);



        }
	}

    public void goToNext()
    {
        
        SceneManager.LoadScene(loadLevel, LoadSceneMode.Single);


    }
}
