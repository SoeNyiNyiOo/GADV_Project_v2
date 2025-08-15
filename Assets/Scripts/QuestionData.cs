using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This makes it show up in the Unity Inspector.
[System.Serializable]

// It's just a simple, custom "container" class for organizing data.
public class QuestionData
{
    public string word; // string to hold the correct word for a single question (e.g., "Dog").
    public Sprite correctImage; // Sprite to hold the correct image that matches the word.
}