using UnityEngine;

public class SaveTest : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField]
    private SaveManager saveManager;

    private void Update()
    {
        if (playerInput.Save)
        {
            Debug.Log("F5 BASILDI!");
            saveManager.SaveGame();
        }

        if (playerInput.Load)
        {
            Debug.Log("F9 BASILDI!");
            saveManager.LoadGame();
        }
    }
}