using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFieldInvokeEnd : MonoBehaviour
{
    public TMP_InputField tMP_InputField;
    
    public void EndEdit(){
        tMP_InputField.onEndEdit.Invoke(tMP_InputField.text);
        Debug.Log("Ending Edit with string value: " + tMP_InputField.text);
    }

    public void AddCharToInputField(string character){
        if(tMP_InputField.characterLimit == tMP_InputField.text.Length){
            return;
        }
        tMP_InputField.text += character;
    }

    public void RemoveCharToInputField(){
        tMP_InputField.text = tMP_InputField.text.Remove(tMP_InputField.text.Length - 1);
    }

}
