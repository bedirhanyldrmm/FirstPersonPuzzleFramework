using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private readonly List<ISaveable> saveables = new();

    private void Awake()
    {
        RegisterSaveables();

        if (PlayerPrefs.GetInt("LoadGameRequested", 0) == 1)
        {
            PlayerPrefs.SetInt("LoadGameRequested", 0);
            PlayerPrefs.Save();

            LoadGame();
        }
    }

    private void RegisterSaveables()
    {
        saveables.Clear();

        MonoBehaviour[] behaviours =
            FindObjectsByType<MonoBehaviour>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

        foreach (MonoBehaviour behaviour in behaviours)
        {
            if (behaviour is ISaveable saveable)
            {
                saveables.Add(saveable);
            }
        }

        Debug.Log($"Registered Saveables: {saveables.Count}");
    }

    public void SaveGame()
    {
        Debug.Log("SAVE GAME REQUESTED");

        SaveSystem.Save(saveables);
    }

    public void LoadGame()
    {
        Debug.Log("LOAD GAME REQUESTED");

        SaveSystem.TryLoad(saveables);
    }
}