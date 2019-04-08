using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonManager : MonoBehaviour {

	public void Start_Button (string newGameLevel)
	{
		SceneManager.LoadScene(newGameLevel);


	}
	public void Exit_Button (string newGameLevel)
	{
		Application.Quit();


	}
}
