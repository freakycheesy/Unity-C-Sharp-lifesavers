using UnityEngine;
using UnityEngine.InputSystem;

public class InputVector2Joystick : Vector2Joystick
{
    public InputActionProperty inputAction = new InputActionProperty();

    void Update()
    {
        joystickX = (float)inputAction.action.ReadValue<Vector2>().x;
        joystickY = (float)inputAction.action.ReadValue<Vector2>().y;
    }
}
