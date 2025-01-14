using Unity.Netcode;
using Unity.Netcode.Components;
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

[RequireComponent(typeof(NetworkObject))]
[RequireComponent(typeof(NetworkTransform))]
[RequireComponent(typeof(Rigidbody))]
public class ElevatorScript : NetworkBehaviour
{
    public Rigidbody rb;
    public TransformLocation[] Floors;

    [Header("Movement Settings")]
    public float elevatorSpeed = 0.5f;

    [Header("States")]
    public NetworkVariable<bool> isMoving = new(writePerm: NetworkVariableWritePermission.Server);
    public NetworkVariable<int> currentFloor = new(writePerm: NetworkVariableWritePermission.Server);
    public NetworkVariable<int> nextFloor = new(writePerm: NetworkVariableWritePermission.Server);
    public NetworkVariable<bool> movingUp = new(true, writePerm: NetworkVariableWritePermission.Server);

    public override void OnNetworkSpawn()
    {
        NetworkObject.ChangeOwnership(NetworkManager.ServerClientId);
        GetComponent<NetworkTransform>().Teleport(Floors[0].WorldPosition, Quaternion.Euler(Floors[0].WorldRotation), transform.localScale);
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    [ServerRpc]
    public void  MoveUpServerRpc(int extraIncrement = 0){
        if(!IsServer) return;
        nextFloor.Value = Mathf.Clamp(currentFloor.Value + extraIncrement, 0, Floors.Length);
    }

    [ServerRpc]
    public void  MoveDownServerRpc(int extraIncrement = 0){
        if(!IsServer) return;
        nextFloor.Value = Mathf.Clamp(currentFloor.Value - extraIncrement, 0, Floors.Length);
    }

    [ContextMenu("Move PingPong")]
    private void MovePingPong(){
        MovePingPongServerRpc();
    }

    [ServerRpc]
    public void  MovePingPongServerRpc(){
        if(!IsServer) return;
        if(currentFloor.Value >= Floors.Length - 1) movingUp.Value = false;
        else if(currentFloor.Value <= 0) movingUp.Value = true;
        if(movingUp.Value) nextFloor.Value++;
        else nextFloor.Value--;
    }

    void Update() {
        if(!IsServer) return;
        isMoving.Value = transform.position != Floors[nextFloor.Value].WorldPosition && transform.position != Floors[nextFloor.Value].WorldPosition;
        if(isMoving.Value) {
            transform.position = Vector3.MoveTowards(transform.position, Floors[nextFloor.Value].WorldPosition, elevatorSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(Floors[nextFloor.Value].WorldRotation), elevatorSpeed * Time.deltaTime);
        }
        else{
            currentFloor = nextFloor;
        }
    }
}