using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class FileBrowser : MonoBehaviour {

    string path;
    public InputField name;
    public Text ftext;

    public void OpenExplorer(){
        path = EditorUtility.OpenFilePanel("Play Midi", "", "mid");
        name.text = path;

}
}
