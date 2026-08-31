using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject keyRedSlot;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private CanvasGroup canvasGroup;

    private bool isOpen;
    public bool IsOpen => isOpen;
    public bool ClosedThisFrame { get; private set; }

    private void Awake()
    {
        isOpen = false;
        SetInventoryVisibility();
        UpdateUI();
    }

    private void Update()
    {
        ClosedThisFrame = false;

        if (playerInput != null && playerInput.Inventory)
        {
            ToggleInventory();
            return;
        }

        if (playerInput != null && playerInput.Pause && isOpen)
        {
            CloseInventory();
            return;
        }

        UpdateUI();
    }

    private void ToggleInventory()
    {
        isOpen = !isOpen;

        SetInventoryVisibility();
    }
    private void CloseInventory()
    {
        isOpen = false;
        ClosedThisFrame = true;

        SetInventoryVisibility();
    }

    private void SetInventoryVisibility()
    {
        canvasGroup.alpha = isOpen ? 1f : 0f;
        canvasGroup.interactable = isOpen;
        canvasGroup.blocksRaycasts = isOpen;

        Time.timeScale = isOpen ? 0f : 1f;
    }

    private void UpdateUI()
    {
        if (inventory == null || keyRedSlot == null)
            return;

        keyRedSlot.SetActive(
            inventory.HasItem("key_red")
        );
    }
}