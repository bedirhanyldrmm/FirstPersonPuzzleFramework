using UnityEngine;

public class InteractionTestObject : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Etkileşim gerçekleşti!");
    }

    public string GetInteractionPrompt()
    {
        return "Etkileş";
    }
}