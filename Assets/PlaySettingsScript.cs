using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script allows the user to switch to the play settings

public class PlaySettingsScript : MonoBehaviour {

    public void playsettingsScenes(){
        SceneManager.LoadScene("PlaySettings"); //Loads the PlaySettings
    }
}
