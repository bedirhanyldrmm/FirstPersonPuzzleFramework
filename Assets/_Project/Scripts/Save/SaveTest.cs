using UnityEngine;

public class SaveTest : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private PlayerInput playerInput;

    private void Update()
    {
        if (playerInput.Save)
        {
            Debug.Log("F5 BASILDI!");
            SaveSystem.SavePlayerPosition(player);
        }

        if (playerInput.Load)
        {
            Debug.Log("F9 BASILDI!");
            SaveSystem.TryLoadPlayerPosition(player);
        }
    }
}