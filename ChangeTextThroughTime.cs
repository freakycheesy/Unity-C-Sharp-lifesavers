using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeTextThroughTime : MonoBehaviour
{
    public TMP_Text tMP_Text;
    public string currentTextString;
    public string[] texts = new string[] { "Loading.", "Loading..", "Loading..."};
    public float[] timePerText = new float[] {0.5f, 0.5f, 0.5f};
    public bool changeText = false;
    public bool canRepeat = true;
    public int currentTextInt = 0;
    void Start()
    {
        changeText = true;
        currentTextInt = 0;
    }

    void Update()
    {   
        if(tMP_Text != null){
            tMP_Text.text = currentTextString;
        }
        if(changeText){
            changeText = false;
            for(int i = 0; i < texts.Length; i++){
                if(currentTextInt == i){
                    Invoke(nameof(ChangeText), timePerText[currentTextInt]);
                }
            }
        }
    }

    public void ChangeText(){        
        if(currentTextInt == texts.Length - 1){
            currentTextString = texts[currentTextInt];
            if(canRepeat)
                currentTextInt = 0; 
            else
                changeText = false;
            Debug.Log(currentTextString);
        }        
        else{
            currentTextString = texts[currentTextInt];  
            Debug.Log(currentTextString);
            currentTextInt++;
        }         
        Debug.Log(currentTextInt);
        changeText = true;
    }
}

