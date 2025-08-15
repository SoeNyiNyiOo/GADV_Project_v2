using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject promptBubble;
    public TextMeshProUGUI promptText; 
    public Image promptImage;          
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public GameObject feedbackBubble;
    public TextMeshProUGUI feedbackText;
    public GameObject pauseMenuPanel;
    public TextMeshProUGUI levelIndicatorText;

    // This is for "Choose Picture" mode. The Question bubble will show the question text.
    public void ShowTextPrompt(string text)
    {
        promptBubble.SetActive(true);
        promptText.gameObject.SetActive(true);
        promptImage.gameObject.SetActive(false);
        promptText.text = text;
    }

    // This is for "Choose Word" mode. The Question bubble will show the image of the word.
    public void ShowImagePrompt(Sprite image)
    {
        promptBubble.SetActive(true);
        promptText.gameObject.SetActive(false);
        promptImage.gameObject.SetActive(true);
        promptImage.sprite = image;
    }
    public void UpdateLevelText(string levelName)
    {
        if (levelIndicatorText != null)
        {
            levelIndicatorText.text = levelName;
        }
    }
    public void UpdateScoreText(int correctAnswers, int totalQuestions)
    {
        scoreText.text = "Score: " + correctAnswers;
    }

    public void UpdateTimerText(float time)
    {
        timerText.text = "Time: " + Mathf.RoundToInt(time);
    }

    // Called in GameManager.cs to hide the prompt bubble
    public void UpdatePromptImage(Sprite newImage)
    {
        if (newImage == null) { promptBubble.SetActive(false); }
        else { promptImage.sprite = newImage; promptBubble.SetActive(true); }
    }

    // This function is used to set the feedback text connect with GameManager.cs
    public void SetFeedbackText(string message, bool isActive)
    {
        feedbackText.text = message;
        feedbackBubble.SetActive(isActive);
    }

    public void TogglePauseMenu(bool isPaused)
    {
        pauseMenuPanel.SetActive(isPaused);
    }
}

