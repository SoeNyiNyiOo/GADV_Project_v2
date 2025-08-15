using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Two game modes
public enum GameMode { ChooseWord, ChoosePicture }

// This is a special Unity attribute that adds a new option to the Assets > Create menu.
// To create instances of this script as ".asset" files in your Project window.
[CreateAssetMenu(fileName = "New Level", menuName = "Whac-A-Word/Level Data")]

// This scriptable object holds all the data for a level, including its name, index, game mode,
public class LevelSO : ScriptableObject
{
    [Header("Level Details")]
    public string levelName;
    public int levelIndex;
    public GameMode gameMode; // Dropdown to select the mode for this level

    [Header("Questions")]
    public List<QuestionData> questions;

    [Header("Distractors")] // Distractor words or images for the level to confuse the player
    public List<string> distractorWords;  // For the "Choose Word" mode
    public List<Sprite> distractorImages; // For the "Choose Picture" mode
}