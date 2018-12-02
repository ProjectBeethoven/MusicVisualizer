using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script allows the user to switch to the play screen

public class playscreenscript : MonoBehaviour {

	public void playscreens(){
	SceneManager.LoadScene("PlayScreen"); //Loads the PlayScreen
	}
}
