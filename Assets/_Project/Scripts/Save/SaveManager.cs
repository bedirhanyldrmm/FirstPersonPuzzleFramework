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
    }

    public void SaveGame()
    {
        SaveSystem.Save(saveables);
    }

    public void LoadGame()
    {
        SaveSystem.TryLoad(saveables);
    }
}