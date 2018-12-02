using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script allows the user to switch to the credits scene

public class CreditsSceneScript : MonoBehaviour {

    public void creditsScenes(){
        SceneManager.LoadScene("CreditsScene"); //Loads CreditsScene
    }
}
