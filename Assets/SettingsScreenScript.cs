using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script allows the user to switch to the settings screen

public class SettingsScreenScript : MonoBehaviour {

    public void settingsScenes(){
        SceneManager.LoadScene("Settings"); //Loads the Settings
    }
}
