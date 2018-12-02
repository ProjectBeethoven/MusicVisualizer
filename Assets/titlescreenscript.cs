using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script allows the user to switch to the title screen

public class titlescreenscript : MonoBehaviour {

	public void titlescreens(){
	SceneManager.LoadScene("TitleScreen"); //Loads the TitleScreen
	}
}
