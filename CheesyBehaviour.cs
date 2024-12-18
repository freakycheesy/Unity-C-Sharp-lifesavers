using UnityEngine;

public class CheesyBehaviour : MonoBehaviour
{
    [HideInInspector]
    public bool activeState = true;

    public Rigidbody GetRigidbody(){
        if(Exists(GetComponent<Rigidbody>())){
            return GetComponent<Rigidbody>();
        }
        else{
            //Debug.LogError($"Could not find rigidbody in {gameObject.name}!");
            return null;
        }
    }
    public Rigidbody2D GetRigidbody2D(){
        if(Exists(GetComponent<Rigidbody2D>())){
            return GetComponent<Rigidbody2D>();
        }
        else{
            //Debug.LogError($"Could not find rigidbody 2D in {gameObject.name}!");
            return null;
        }
    }

    private bool tempActiveState = true;

    private void OnDisable(){
        tempActiveState = activeState;
        activeState = false;
    }

    private void OnEnable(){
        activeState = tempActiveState;
    }

    public static bool Exists(Object @unityObject = null, object @csObject = null){
        return @unityObject != null || @csObject != null;
    }

    public static bool Compare(object obj1 = null, object obj2 = null, bool checkIfNotEqual = false){
        bool compareStatement = checkIfNotEqual ? obj1 != obj2 : obj1 == obj2;
        return compareStatement;
    }
    public static bool ExistsInArray(object targetObj1 = null, Object targetObj2 = null, object[] arrayObj1 = null, Object[] arrayObj2 = null){
        if(Exists(null, targetObj1)){
            foreach(var v in arrayObj1){
                if(targetObj1 == v){
                    return true;
                }
            }
        }
        else if(Exists(targetObj2, null)){
            foreach(var v in arrayObj2){
                if(targetObj2 == v){
                    return true;
                }
            }
        }
        //Debug.LogError($"You should only have 1 target object and 1 array object in ExistsInArray()");
        return false;
    }
}
