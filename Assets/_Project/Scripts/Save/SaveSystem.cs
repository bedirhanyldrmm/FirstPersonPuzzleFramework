using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private const string SaveFileName = "save.json";

    private static string SavePath =>
        Path.Combine(Application.persistentDataPath, SaveFileName);

    public static void Save(ISaveable saveable)
    {
        object state = saveable.CaptureState();

        string json = JsonUtility.ToJson(state, true);
        File.WriteAllText(SavePath, json);

        Debug.Log("GAME SAVED!");
        Debug.Log($"Save Path: {SavePath}");
    }

    public static bool TryLoad(ISaveable saveable)
    {
        if (!File.Exists(SavePath))
        {
            Debug.LogWarning("No save file found.");
            return false;
        }

        string json = File.ReadAllText(SavePath);

        PlayerSaveData data =
            JsonUtility.FromJson<PlayerSaveData>(json);

        Debug.Log("GAME LOADING!");
        Debug.Log($"Saved Position From File: {data.position}");

        MonoBehaviour saveableBehaviour =
            saveable as MonoBehaviour;

        CharacterController controller = null;

        if (saveableBehaviour != null)
        {
            controller =
                saveableBehaviour.GetComponent<CharacterController>();
        }

        if (controller != null)
        {
            controller.enabled = false;
        }

        saveable.RestoreState(data);

        if (controller != null)
        {
            controller.enabled = true;
        }

        Debug.Log("GAME LOADED!");

        return true;
    }
}