using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holding data that needs to persist between scene changes. (Static)
public static class GameData
{
    // A static boolean to store whether the player met the win condition.
    // 'static' means this variable belongs to the class itself, not an instance of the class.
    public static bool PlayerWon;

    // A static string to store the final score (e.g., "3 / 5").
    public static string FinalScore;

    // A static enum to store which game mode was just played.
    // This allows the EndScene to know which level selection screen to return to.
    public static GameMode LastPlayedMode;
}
