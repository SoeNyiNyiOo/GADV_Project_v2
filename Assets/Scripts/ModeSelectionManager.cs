using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This class manages the mode selection screen where players can choose between Pictures or Words mode.
public class ModeSelectionManager : MonoBehaviour
{
    public void StartPicturesMode()
    {
        SceneManager.LoadScene("PicturesLevelsSelection");
    }

    public void StartWordsMode()
    {
        SceneManager.LoadScene("WordsLevelsSelection");
    }

    // Function for the back arrow button 
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
