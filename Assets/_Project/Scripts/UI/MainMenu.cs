using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "SampleScene";

    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void LoadGame()
    {
        PlayerPrefs.SetInt("LoadGameRequested", 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT GAME");

        Application.Quit();
    }
}