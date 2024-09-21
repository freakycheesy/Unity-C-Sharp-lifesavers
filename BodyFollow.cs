using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyFollow : MonoBehaviour
{
    [SerializeField] Vector3 offset = new Vector3(0f, -0.4f, 0f);
    [SerializeField] Vector3 rotationalOffset = new Vector3(0f, 180, 0f);
    [SerializeField] Transform target;

    private void LateUpdate() {
        Vector3 targetPosition = offset;
        transform.localPosition = targetPosition;
        Quaternion yaw = Quaternion.Euler(new Vector3(0, target.eulerAngles.y, 0) + rotationalOffset);
        transform.rotation = yaw;
        transform.parent = target;
    }
}

