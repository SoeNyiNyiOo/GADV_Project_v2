using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


// This class manages the level selection for the Words mode.
public class WordsLevelSelectionManager : MonoBehaviour
{
    public void LoadLevel(LevelSO levelToLoad)
    {
        // UseLevelLoader to set the chosen level data
        LevelLoader.SetLevel(levelToLoad);
        SceneManager.LoadScene("Levels");
    }

    // Arrow Back button function
    public void GoToModeSelection()
    {
        SceneManager.LoadScene("ModeSelection");
    }
}
