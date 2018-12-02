using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script allows the user to switch to the load screen

public class loadscreenscript : MonoBehaviour {

    public void loadscreens(){
    SceneManager.LoadScene("LoadScreen"); //Loads the LoadScreen
    }
}
