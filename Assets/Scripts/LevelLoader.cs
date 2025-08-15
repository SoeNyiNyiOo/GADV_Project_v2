using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For carrying data from one scene to another.
public static class LevelLoader
{
    // Hold the Level Data (LevelSO) the player chose.
    // 'public get;' any other script can read what level is selected.
    // 'private set;' only this LevelLoader script can change which level is selected.
    // This prevents other scripts from accidentally changing the level data.
    public static LevelSO SelectedLevel { get; private set; }

    // It takes the level data asset (levelData) that the player clicked on
    // and stores it in static SelectedLevel property, ready for the GameManager to read.
    public static void SetLevel(LevelSO levelData)
    {
        SelectedLevel = levelData;
    }
}
