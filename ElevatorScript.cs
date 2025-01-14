using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(ElevatorScript))]
public class ElevatorScriptEditor : Editor {
    public override void OnInspectorGUI() {
        ElevatorScript myScript = (ElevatorScript)target;

        if (GUILayout.Button("Move PingPong Style")) {
            myScript.MovePingPongServerRpc();
        }

        if (GUILayout.Button("Move Up")) {
            myScript.MoveUpServerRpc();
        }

        if (GUILayout.Button("Move Down")) {
            myScript.MoveDownServerRpc();
         }

        // Draw the rest of the inspector below the custom title
        base.OnInspectorGUI();
    }
}
#endif

[System.Serializable]
public class TransformLocation {
    public Vector3 WorldPosition;
    public Vector3 WorldRotation;
}

[RequireComponent(typeof(Rigidbody))]
public class ElevatorScript : MonoBehaviour
{
    public Rigidbody rb;
    public Transform[] Floors;

    [Header("Movement Settings")]
    public float elevatorSpeed = 0.5f;

    [Header("States")]
    public bool isMoving;
    public int currentFloor;
    public int nextFloor;    
public bool movingUp = true;

    private void Start()
    {   
        transform.position = Floors[0].position;
        transform.rotation = Floors[0].rotation;      rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }

        public void  MoveUpServerRpc(int extraIncrement = 0){
        nextFloor = Mathf.Clamp(currentFloor+ extraIncrement, 0, Floors.Length);
    }

        public void  MoveDownServerRpc(int extraIncrement = 0){
              nextFloor = Mathf.Clamp(currentFloor - extraIncrement, 0, Floors.Length);
    }

    [ContextMenu("Move PingPong")]
    private void MovePingPong(){
        MovePingPongServerRpc();
    }

        public void  MovePingPongServerRpc(){
        
        if(currentFloor >= Floors.Length - 1) movingUp = false;
        else if(currentFloor <= 0) movingUp = true;
        if(movingUp) nextFloor++;
        else nextFloor--;
    }

    void Update() {
                isMoving = transform.position != Floors[nextFloor].position && transform.rotation != Floors[nextFloor].rotation;
        if(isMoving.Value) {
            transform.position = Vector3.MoveTowards(transform.position, Floors[nextFloor].position, elevatorSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(Floors[nextFloor.Value].eulerAngles), elevatorSpeed * Time.deltaTime);
        }
        else{
            currentFloor = nextFloor;
        }
    }
}


