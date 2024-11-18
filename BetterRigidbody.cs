using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(BetterRigidbody))]
public class BetterRigidbodyEditor : Editor {
    public override void OnInspectorGUI() {
        // Draw the default inspector fields
        DrawDefaultInspector();

        // Add a button to the inspector
        BetterRigidbody myScript = (BetterRigidbody)target;
        if (GUILayout.Button("Update Rigidbody Values")) {
            myScript.UpdateFreeze();
        }
    }
}

#endif

[ExecuteAlways]
[RequireComponent(typeof(Rigidbody))]
public class BetterRigidbody : MonoBehaviour
{
    [Header("Mass")]
    public bool massIsAffectedByLocalScale = true;

    [Header("Velocity")]
    public float maxLinearVelocity = 20f;
    public float maxAngluarVelocity = 20f;

    [Header("Constraints")]
    [Header("-Settings")]
    public bool changePositionValuesOnUpdate = true;
    [Header("-Values")]
    public bool freezePosition = false;
    public bool freezeRotation = false;

    [HideInInspector]
    public Rigidbody rb;

    [HideInInspector]
    public float divideValue = 1.732051f;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(massIsAffectedByLocalScale)
            rb.mass = transform.localScale.magnitude / divideValue;
        rb.maxLinearVelocity = maxLinearVelocity;
        rb.maxAngularVelocity = maxAngluarVelocity;
    }

    public void UpdateFreeze() {
        RigidbodyConstraints rigidbodyConstraints = rb.constraints;
        if (changePositionValuesOnUpdate) {
            if (freezePosition) {
                rb.constraints = RigidbodyConstraints.FreezePosition;
            }
            else {
                rb.constraints = RigidbodyConstraints.None;
            }
        }
        rb.freezeRotation = freezeRotation;
    }
}
