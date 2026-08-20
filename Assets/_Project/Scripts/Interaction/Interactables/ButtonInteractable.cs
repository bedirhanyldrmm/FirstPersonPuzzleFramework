using UnityEngine;
using UnityEngine.Events;

public class ButtonInteractable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Transform buttonVisual;

    [SerializeField]
    private float pressedDistance = 0.1f;

    [SerializeField]
    private float pressSpeed = 8f;
    [SerializeField]
    private UnityEvent onPressed;

    [SerializeField]
    private UnityEvent onReleased;

    private bool isPressed;

    private Vector3 initialLocalPosition;
    private Vector3 pressedLocalPosition;

    private void Awake()
    {
        initialLocalPosition = buttonVisual.localPosition;

        pressedLocalPosition = initialLocalPosition;
        pressedLocalPosition.y -= pressedDistance;
    }

    private void Update()
    {
        Vector3 targetPosition = isPressed
            ? pressedLocalPosition
            : initialLocalPosition;

        buttonVisual.localPosition = Vector3.Lerp(
            buttonVisual.localPosition,
            targetPosition,
            pressSpeed * Time.deltaTime
        );
    }

    public void Interact()
    {
        isPressed = !isPressed;

        if (isPressed)
        {
            onPressed?.Invoke();
        }
        else
        {
            onReleased?.Invoke();
        }
    }


    public string GetInteractionPrompt()
    {
        return isPressed ? "Butonu Bırak" : "Butona Bas";
    }
}