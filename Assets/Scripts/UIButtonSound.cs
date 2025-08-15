using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// This script requires that the GameObject has a Button component
[RequireComponent(typeof(Button))]
public class UIButtonSound : MonoBehaviour
{
    void Start()
    {
        // Get the Button component on this same GameObject
        Button button = GetComponent<Button>();

        // Add a "listener" to the button's onClick event.
        // This tells the button: "When you are clicked, run the PlayButtonClickSound function."
        button.onClick.AddListener(() => SoundManager.Instance.PlayButtonClickSound());
    }
}
