using UnityEngine;

public class PlayerSaveable : MonoBehaviour, ISaveable
{
    public object CaptureState()
    {
        return new PlayerSaveData
        {
            position = transform.position,
            rotation = transform.rotation
        };
    }

    public void RestoreState(object state)
    {
        PlayerSaveData data = (PlayerSaveData)state;

        transform.position = data.position;
        transform.rotation = data.rotation;
    }
}