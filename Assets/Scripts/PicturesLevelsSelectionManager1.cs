using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

// This class manages the level selection for the Pictures mode.
public class PicsLevelSelectionManager : MonoBehaviour
{
    public void LoadLevel(LevelSO levelToLoad)
    {
        // Use LevelLoader to set the chosen level data
        LevelLoader.SetLevel(levelToLoad);
        SceneManager.LoadScene("Levels");
    }

    // Arrow Back button function
    public void GoToModeSelection()
    {
        SceneManager.LoadScene("ModeSelection");
    }
}
