using UnityEngine;

public class PlayerSaveable : MonoBehaviour, ISaveable
{
    public object CaptureState()
    {
        Debug.Log($"PLAYER SAVE POSITION: {transform.position}");

        return new PlayerSaveData
        {
            position = transform.position,
            rotation = transform.rotation
        };
    }

    public void RestoreState(object state)
    {
        PlayerSaveData data = (PlayerSaveData)state;

        CharacterController controller = GetComponent<CharacterController>();

        if (controller != null)
        {
            controller.enabled = false;
        }

        transform.position = data.position;
        transform.rotation = data.rotation;

        Debug.Log($"AFTER POSITION SET: {transform.position}");

        if (controller != null)
        {
            controller.enabled = true;
        }

        Debug.Log($"AFTER CONTROLLER ENABLE: {transform.position}");
    }
}