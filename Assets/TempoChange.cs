using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class TempoChange : MonoBehaviour {

    public InputField initTemp; //This is for the first input field with the initial tempo
    public InputField newTemp; //This is for the second input field with the new tempo


	public void sliderChange(float newValue){

        float tempSub = 0;//This is the value to add to the original tempo
        float fTemp = float.Parse(initTemp.text); //The converts the string from initTemp to a float

        tempSub = newValue; //This sets tempSub to newValue
        newValue = fTemp * tempSub; //This is the final tempo

        //This series of if and else statements is to keep the BPM within a certain range. In this case 5 BPM to 400 BPM
        if(newValue < 5){
            newValue = 5;
        }
        else if(newValue > 400){
            newValue = 400;
        }

        newTemp.text = newValue.ToString();//Converts newValue to string and sets newTemps text to newValue
	}
}
