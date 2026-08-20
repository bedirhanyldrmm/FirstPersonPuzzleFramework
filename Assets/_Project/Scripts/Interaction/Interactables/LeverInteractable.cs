using UnityEngine;
using UnityEngine.Events;

public class LeverInteractable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Transform leverVisual;

    [SerializeField]
    private float activeAngle = 45f;

    [SerializeField]
    private float rotateSpeed = 8f;
    [SerializeField]
    private UnityEvent onActivated;

    [SerializeField]
    private UnityEvent onDeactivated;

    private bool isActive;

    private Quaternion initialRotation;
    private Quaternion activeRotation;

    private void Awake()
    {
        initialRotation = leverVisual.localRotation;

        activeRotation = initialRotation
            * Quaternion.Euler(activeAngle, 0f, 0f);
    }

    private void Update()
    {
        Quaternion targetRotation = isActive
            ? activeRotation
            : initialRotation;

        leverVisual.localRotation = Quaternion.Slerp(
            leverVisual.localRotation,
            targetRotation,
            rotateSpeed * Time.deltaTime
        );
    }

    public void Interact()
    {
        isActive = !isActive;

        if (isActive)
        {
            onActivated?.Invoke();
        }
        else
        {
            onDeactivated?.Invoke();
        }
    
}

    public string GetInteractionPrompt()
    {
        return isActive
            ? "Leverı Kapat"
            : "Leverı Aç";
    }
}