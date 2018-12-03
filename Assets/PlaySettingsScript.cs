using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySettingsScript : MonoBehaviour {

    public void playsettingsScenes(){
        SceneManager.LoadScene("PlaySettings");
    }
}
