using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private HashSet<string> items = new HashSet<string>();

    public void AddItem(string itemId)
    {
        items.Add(itemId);
    }

        

    public bool HasItem(string itemId)
    {
        bool hasItem = items.Contains(itemId);

        

        return hasItem;
    }


    public void RemoveItem(string itemId)
    {
        items.Remove(itemId);
    }
}