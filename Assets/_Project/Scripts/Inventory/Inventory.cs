using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private HashSet<string> items = new HashSet<string>();

    public void AddItem(string itemId)
    {
        if (items.Add(itemId))
        {
            Debug.Log($"Item added to inventory: {itemId}");
        }
    }

    public bool HasItem(string itemId)
    {
        return items.Contains(itemId);
    }

    public void RemoveItem(string itemId)
    {
        items.Remove(itemId);
    }

    public List<string> GetItems()
    {
        return new List<string>(items);
    }

    public void SetItems(List<string> savedItems)
    {
        items.Clear();

        foreach (string itemId in savedItems)
        {
            items.Add(itemId);
        }
    }
}