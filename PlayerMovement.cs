using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    private Vector2 lookInput;

    public Transform cameraTransform;
    public float speed = 6f;
    public float jumpForce = 2f;
    private CharacterController controller;
    private Vector3 velocity;
    private bool jump;

    private void Start() {
        controller = gameObject.GetComponent<CharacterController>();
        if (controller == null) {
            controller = gameObject.AddComponent<CharacterController>();
        }
        controller.minMoveDistance = 0f;
    }

    public void OnMove(InputAction.CallbackContext callbackContext) {
        moveInput = callbackContext.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext callbackContext) {
        jump = callbackContext.action.IsPressed();
    }
    public void OnLook(InputAction.CallbackContext callbackContext) {
        lookInput = callbackContext.ReadValue<Vector2>();
    }

    private void Update() {
        Move();
        Look();
    }

    private void Move() {
        bool isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        float x = moveInput.x;
        float z = moveInput.y;

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (jump && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    public float sens = 100f;
    private float xRotation = 0f;

    private void Look() {
        xRotation -= lookInput.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * lookInput.x);
    }
}
