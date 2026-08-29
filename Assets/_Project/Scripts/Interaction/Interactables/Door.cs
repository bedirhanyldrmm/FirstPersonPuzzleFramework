using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField]
    private float openSpeed = 3f;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private InteractionFeedbackUI feedbackUI;

    [SerializeField]
    private string requiredKeyId = "key_red";
    [SerializeField]
    private bool requiresKey = true;

    [SerializeField]
    private string requiredKeyMessage = "Kırmızı Anahtar Gerekli";

    [SerializeField]
    private bool consumeKey;

    private bool isOpen;
    public bool IsOpen => isOpen;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    private void Awake()
    {
        closedRotation = transform.parent.rotation;
    }

    private void Update()
    {
        Quaternion targetRotation = isOpen
            ? openRotation
            : closedRotation;

        transform.parent.rotation = Quaternion.Slerp(
            transform.parent.rotation,
            targetRotation,
            openSpeed * Time.deltaTime
        );
    }

    public void Interact()
    {
        if (!isOpen)
        {
            if (!CanOpen())
                return;

            CalculateOpenRotation();

            isOpen = true;
        }
        else
        {
            isOpen = false;
        }
    }

    public void OpenDoor()
    {
        if (isOpen)
            return;

        if (!CanOpen())
            return;

        CalculateOpenRotation();

        isOpen = true;
    }
    public void CloseDoor()
    {
        if (!isOpen)
            return;

        isOpen = false;
    }

    private bool CanOpen()
    {
        if (!requiresKey)
            return true;

        if (!inventory.HasItem(requiredKeyId))
        {
            feedbackUI.Show(
                "Kilitli - " + requiredKeyMessage
            );

            return false;
        }

        if (consumeKey)
        {
            inventory.RemoveItem(requiredKeyId);
        }

        return true;
    }

    private void CalculateOpenRotation()
    {
        Vector3 directionToPlayer =
            player.position - transform.parent.position;

        float dot = Vector3.Dot(
            transform.parent.forward,
            directionToPlayer
        );

        float targetAngle = dot > 0f
            ? 90f
            : -90f;

        openRotation = closedRotation * Quaternion.Euler(
            0f,
            targetAngle,
            0f
        );
    }

    public string GetInteractionPrompt()
    {
        return isOpen
            ? "E - CLOSE DOOR"
            : "E - OPEN DOOR";
    }
}