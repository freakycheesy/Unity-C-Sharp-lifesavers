using UnityEngine;

public class Vector2Joystick : CheesyBehaviour
{
    [Header("Vector2 Joystick")]
    [Range(0.1f, 100f)]
    public float joystickMultiplier = 1;

    public float joystickX = 0;
    public float joystickY = 0;

    public Vector2 GetJoystickPosition(){
        return new Vector2(joystickX, joystickY) * joystickMultiplier;
    }
}
