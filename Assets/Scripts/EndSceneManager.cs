using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class EndSceneManager : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    public TextMeshProUGUI winScoreText;
    public TextMeshProUGUI loseScoreText;

    void Start()
    {
        // Read the data from our static class
        /* There are two panels in the scene: Win and Lose.
         If the player win the game, Win panel set to true, losepanel to false and vice versa
        Show the score, calls the PlayWinSound() from SoundManager.cs */
        if (GameData.PlayerWon)
        {
            winPanel.SetActive(true);
            losePanel.SetActive(false);
            winScoreText.text = "Score: " + GameData.FinalScore;
            SoundManager.Instance.PlayWinSound();
        }
        else
        {
            winPanel.SetActive(false);
            losePanel.SetActive(true);
            loseScoreText.text = "Score: " + GameData.FinalScore;
            SoundManager.Instance.PlayLoseSound();
        }
    }
    // This function is called for "Try Again button" in the End Scene.
    public void RetryLevel()
    {
        SoundManager.Instance.StopSFX(); 
        SceneManager.LoadScene("Levels");
    }

    // This function is called for "Continue" button in the End Scene.
    // It checks the last played mode from GameData.cs and loads the appropriate level selection scene.
    // e.g. If the last played mode was "Choose Word", it loads "WordsLevelsSelection".
    public void ContinueToLevelSelection()
    {
        SoundManager.Instance.StopSFX(); 

        if (GameData.LastPlayedMode == GameMode.ChooseWord)
        {
            SceneManager.LoadScene("WordsLevelsSelection");
        }
        else if (GameData.LastPlayedMode == GameMode.ChoosePicture)
        {
            SceneManager.LoadScene("PicturesLevelsSelection");
        }
    }
}

