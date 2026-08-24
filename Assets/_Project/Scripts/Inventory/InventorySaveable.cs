using System.Collections.Generic;
using UnityEngine;

public class InventorySaveable : MonoBehaviour, ISaveable
{
    private Inventory inventory;

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    public object CaptureState()
    {
        return new InventorySaveData
        {
            items = inventory.GetItems()
        };
    }

    public void RestoreState(object state)
    {
        InventorySaveData data = (InventorySaveData)state;

        inventory.SetItems(data.items);
    }
}