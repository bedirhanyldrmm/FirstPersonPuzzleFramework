using UnityEngine;

public class KeyItem : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string itemId = "key_red";

    [SerializeField]
    private Inventory inventory;

    public void Interact()
    {
        inventory.AddItem(itemId);

        

        Destroy(gameObject);
    }
    public string GetInteractionPrompt()
    {
        return "Anahtarı Al";
    }
}