using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        // Loads the scene where the player chooses the game mode
        SceneManager.LoadScene("ModeSelection");
    }

    public void ExitGame()
    {
        // This quits the application. It only works in a built game, not the editor.
        Debug.Log("Exit button clicked!");
        Application.Quit();
    }
}
