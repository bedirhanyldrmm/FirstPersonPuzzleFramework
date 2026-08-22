using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private const string SaveFileName = "save.json";

    private static string SavePath =>
        Path.Combine(Application.persistentDataPath, SaveFileName);

    public static void SavePlayerPosition(Transform player)
    {
        SaveData data = new SaveData
        {
            playerPosition = player.position,
            playerRotation = player.rotation
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);

        Debug.Log("GAME SAVED!");
        Debug.Log($"Saved Position: {player.position}");
    }

    public static bool TryLoadPlayerPosition(Transform player)
    {
        if (!File.Exists(SavePath))
        {
            Debug.LogWarning("No save file found.");
            return false;
        }

        string json = File.ReadAllText(SavePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        Debug.Log("GAME LOADING!");
        Debug.Log($"Saved Position From File: {data.playerPosition}");
        Debug.Log($"Current Position Before Load: {player.position}");

        CharacterController controller =
            player.GetComponent<CharacterController>();

        if (controller != null)
        {
            controller.enabled = false;
        }

        player.position = data.playerPosition;
        player.rotation = data.playerRotation;

        if (controller != null)
        {
            controller.enabled = true;
        }

        Debug.Log($"Current Position After Load: {player.position}");

        return true;
    }
}