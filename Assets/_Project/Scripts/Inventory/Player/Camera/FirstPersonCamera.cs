using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    private PlayerInput playerInput;
    private Transform cameraTransform;
    private float pitch;

    [SerializeField]
    private float sensitivity = 2f;

    [SerializeField]
    private float minPitch = -80f;

    [SerializeField]
    private float maxPitch = 80f;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }
    private void Update()
    {
        Vector2 lookInput = playerInput.Look;
        
        float mouseX = lookInput.x * sensitivity;
        float mouseY = lookInput.y * sensitivity;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}