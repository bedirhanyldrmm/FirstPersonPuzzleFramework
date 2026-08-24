using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private const string SaveFileName = "save.json";

    private static string SavePath =>
        Path.Combine(Application.persistentDataPath, SaveFileName);

    [Serializable]
    private class SaveEntry
    {
        public string id;
        public string state;
    }

    [Serializable]
    private class SaveFile
    {
        public List<SaveEntry> entries = new();
    }

    public static void Save(List<ISaveable> saveables)
    {
        SaveFile saveFile = new SaveFile();

        foreach (ISaveable saveable in saveables)
        {
            SaveEntry entry = new SaveEntry
            {
                id = GetSaveId(saveable),
                state = JsonUtility.ToJson(saveable.CaptureState())
            };

            saveFile.entries.Add(entry);
        }

        string json = JsonUtility.ToJson(saveFile, true);
        File.WriteAllText(SavePath, json);

        Debug.Log("GAME SAVED!");
        Debug.Log($"Save Path: {SavePath}");
        Debug.Log($"Saved Objects: {saveFile.entries.Count}");
    }

    public static bool TryLoad(List<ISaveable> saveables)
    {
        if (!File.Exists(SavePath))
        {
            Debug.LogWarning("No save file found.");
            return false;
        }

        string json = File.ReadAllText(SavePath);
        SaveFile saveFile = JsonUtility.FromJson<SaveFile>(json);

        if (saveFile == null || saveFile.entries == null)
        {
            Debug.LogWarning("Save file is invalid.");
            return false;
        }

        Debug.Log("GAME LOADING!");
        Debug.Log($"Saved Objects: {saveFile.entries.Count}");

        foreach (ISaveable saveable in saveables)
        {
            string id = GetSaveId(saveable);

            SaveEntry entry = saveFile.entries.Find(
                savedEntry => savedEntry.id == id
            );

            if (entry == null)
            {
                Debug.LogWarning($"No save data found for: {id}");
                continue;
            }

            object state = CreateState(saveable, entry.state);

            saveable.RestoreState(state);

            Debug.Log($"Loaded: {id}");
        }

        Debug.Log("GAME LOADED!");

        return true;
    }

    private static string GetSaveId(ISaveable saveable)
    {
        return saveable.GetType().Name;
    }

    private static object CreateState(ISaveable saveable, string json)
    {
        Debug.Log($"STATE JSON: {json}");

        if (saveable is InventorySaveable)
        {
            return JsonUtility.FromJson<InventorySaveData>(json);
        }

        if (saveable is PlayerSaveable)
        {
            PlayerSaveData data =
                JsonUtility.FromJson<PlayerSaveData>(json);

            Debug.Log($"PARSED PLAYER POSITION: {data.position}");

            return data;
        }

        if (saveable is DoorSaveable)
        {
            return JsonUtility.FromJson<DoorSaveData>(json);
        }

        throw new InvalidOperationException(
            $"No state type registered for {saveable.GetType().Name}"
        );
    }
}