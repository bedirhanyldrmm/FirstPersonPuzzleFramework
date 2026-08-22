using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonMovement : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerInput playerInput;
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float sprintSpeed = 8f;
    [SerializeField]
    private float gravity = -20f;
    [SerializeField]
    private float jumpHeight = 1.5f;

    private float verticalVelocity;
    private void Update()
    {
        if (characterController.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        if (playerInput.Jump && characterController.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(-2f * gravity * jumpHeight);
        }

        Vector2 input = playerInput.Move;
        input = Vector2.ClampMagnitude(input, 1f);

        float currentSpeed = playerInput.Sprint
            ? sprintSpeed
            : moveSpeed;

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 horizontalMovement =
            transform.right * input.x +
            transform.forward * input.y;

        horizontalMovement *= currentSpeed;

        Vector3 verticalMovement =
            Vector3.up * verticalVelocity;

        characterController.Move(
            (horizontalMovement + verticalMovement) * Time.deltaTime);
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    

    
    
}
