using UnityEngine;
using UnityEngine.Events;


public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onPressed;

    [SerializeField]
    private UnityEvent onReleased;
    [SerializeField]
    private Transform plateVisual;

    [SerializeField]
    private float pressedDistance = 0.1f;

    [SerializeField]
    private float pressSpeed = 8f;

    private bool isPressed;

    private Vector3 initialLocalPosition;
    private Vector3 pressedLocalPosition;

    private void Awake()
    {
        initialLocalPosition = plateVisual.localPosition;

        pressedLocalPosition = initialLocalPosition;
        pressedLocalPosition.y -= pressedDistance;
    }

    private void Update()
    {
        Vector3 targetPosition = isPressed
            ? pressedLocalPosition
            : initialLocalPosition;

        plateVisual.localPosition = Vector3.Lerp(
            plateVisual.localPosition,
            targetPosition,
            pressSpeed * Time.deltaTime
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PlayerInput>() == null)
            return;

        if (isPressed)
            return;

        isPressed = true;

        onPressed?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<PlayerInput>() == null)
            return;

        if (!isPressed)
            return;

        isPressed = false;

        onReleased?.Invoke();
    }
}