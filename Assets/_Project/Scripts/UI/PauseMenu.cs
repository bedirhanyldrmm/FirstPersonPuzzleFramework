using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private SaveManager saveManager;
    [SerializeField] private GameObject saveFeedbackText;

    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private bool isPaused;

    private void Awake()
    {
        pausePanel.SetActive(false);
        saveFeedbackText.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (playerInput != null && playerInput.Pause)
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SaveGame()
    {
        if (saveManager != null)
        {
            saveManager.SaveGame();

            saveFeedbackText.SetActive(true);

            StopCoroutine(nameof(HideSaveFeedback));
            StartCoroutine(nameof(HideSaveFeedback));
        }
        else
        {
            Debug.LogError("SaveManager reference is NULL!");
        }
    }

    private IEnumerator HideSaveFeedback()
    {
        yield return new WaitForSecondsRealtime(2f);

        saveFeedbackText.SetActive(false);
    }

    public void LoadGame()
    {
        if (saveManager != null)
        {
            saveManager.LoadGame();

            isPaused = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            Debug.LogError("SaveManager reference is NULL!");
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;

        SceneManager.LoadScene(mainMenuSceneName);
    }
}