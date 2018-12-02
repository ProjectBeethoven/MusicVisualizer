using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

//This script allows the user to access files on their computer and load them into the software

public class FileBrowser : MonoBehaviour {

    string path; //File path
    public InputField name; //Input field showing final path
    public Text ftext;

    //This part opens the file browser and gets the path for the chosen MIDI file
    public void OpenExplorer(){
        path = EditorUtility.OpenFilePanel("Play Midi", "", "mid");
        name.text = path;

}
}
