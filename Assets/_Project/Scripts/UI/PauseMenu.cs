using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private SaveManager saveManager;
    [SerializeField] private GameObject saveFeedbackText;

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
        Debug.Log("SAVE BUTTON CLICKED!");

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
        Debug.Log("LOAD BUTTON CLICKED!");

        if (saveManager != null)
        {
            saveManager.LoadGame();

            isPaused = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1f;

            Debug.Log("PAUSE MENU CLOSED AFTER LOAD!");
        }
        else
        {
            Debug.LogError("SaveManager reference is NULL!");
        }
    }
}