using TMPro;
using UnityEngine;

public class InteractionFeedbackUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI feedbackText;

    [SerializeField]
    private float displayDuration = 2f;

    private float hideTimer;

    private void Awake()
    {
        Hide();
    }

    private void Update()
    {
        if (hideTimer > 0f)
        {
            hideTimer -= Time.deltaTime;

            if (hideTimer <= 0f)
            {
                Hide();
            }
        }
    }

    public void Show(string message)
    {
        feedbackText.text = message;
        feedbackText.gameObject.SetActive(true);

        hideTimer = displayDuration;
    }

    public void Hide()
    {
        feedbackText.gameObject.SetActive(false);
        hideTimer = 0f;
    }
}