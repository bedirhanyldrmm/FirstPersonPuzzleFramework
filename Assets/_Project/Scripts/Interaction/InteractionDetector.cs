using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;

    [SerializeField]
    private float interactionDistance = 3f;

    [SerializeField]
    private float sphereRadius = 0.2f;

    [SerializeField]
    private InteractionPromptUI interactionPromptUI;

    private IInteractable currentInteractable;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        DetectInteractable();
        HandleInteraction();
    }

    private void DetectInteractable()
    {
        IInteractable interactable = null;

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.SphereCast(
            ray,
            sphereRadius,
            out RaycastHit hit,
            interactionDistance))
        {
            interactable = hit.collider.GetComponentInParent<IInteractable>();
        }

        if (interactable != currentInteractable)
        {
            currentInteractable = interactable;

            if (currentInteractable != null)
            {
                interactionPromptUI.Show(
                    currentInteractable.GetInteractionPrompt()
                );
            }
            else
            {
                interactionPromptUI.Hide();
            }
        }
    }

    private void HandleInteraction()
    {
        if (!playerInput.Interact)
        {
            return;
        }

        if (currentInteractable == null)
        {
            return;
        }

        currentInteractable.Interact();
    }
}