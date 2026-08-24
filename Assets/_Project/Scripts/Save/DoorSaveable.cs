using UnityEngine;

public class DoorSaveable : MonoBehaviour, ISaveable
{
    [SerializeField]
    private Door door;

    public object CaptureState()
    {
        return new DoorSaveData
        {
            isOpen = door.IsOpen
        };
    }

    public void RestoreState(object state)
    {
        DoorSaveData data = (DoorSaveData)state;

        if (data.isOpen)
        {
            door.OpenDoor();
        }
        else
        {
            door.CloseDoor();
        }

        Debug.Log($"Door restored. Open: {data.isOpen}");
    }
}