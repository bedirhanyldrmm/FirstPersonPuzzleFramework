using UnityEngine;

public class SaveTest : MonoBehaviour
{
    [SerializeField]
    private PlayerSaveable playerSaveable;

    [SerializeField]
    private PlayerInput playerInput;

    private void Update()
    {
        if (playerInput.Save)
        {
            Debug.Log("F5 BASILDI!");
            SaveSystem.Save(playerSaveable);
        }

        if (playerInput.Load)
        {
            Debug.Log("F9 BASILDI!");
            SaveSystem.TryLoad(playerSaveable);
        }
    }
}