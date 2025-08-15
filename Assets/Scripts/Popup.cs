using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{
    public bool isCorrect = false;

    // --- PUBLIC REFERENCES ---
    public TextMeshPro textMesh;
    public SpriteRenderer imageRenderer;

    [Header("Image Settings")]
    public float maxWidth = 1f;   // Maximum world width
    public float maxHeight = 1f;  // Maximum world height

    private GameManager gameManager; // Cached reference

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // --- Set up the popup as a word or picture ---
    // The Popup prefabs have both TextMeshPro and Sprte 2D Image
    // For word popups, disable the image renderer and enable the text mesh.
    public void SetupAsWord(string wordToShow, bool isThisTheCorrectAnswer)
    {
        isCorrect = isThisTheCorrectAnswer;

        textMesh.gameObject.SetActive(true);
        imageRenderer.gameObject.SetActive(false);

        textMesh.text = wordToShow;
    }

    // for picture popups, disable the text mesh and enable the image renderer.
    public void SetupAsPicture(Sprite imageToShow, bool isThisTheCorrectAnswer)
    {
        isCorrect = isThisTheCorrectAnswer;

        textMesh.gameObject.SetActive(false);
        imageRenderer.gameObject.SetActive(true);

        imageRenderer.sprite = imageToShow;

        // --- Fit image inside box while preserving aspect ratio ---
        if (imageRenderer.sprite != null)
        {
            Vector2 spriteSize = imageRenderer.sprite.bounds.size;

            // Calculate scale factors for width and height
            float scaleX = maxWidth / spriteSize.x;
            float scaleY = maxHeight / spriteSize.y;

            // Use the smaller scale to ensure it fits inside both width and height
            float finalScale = Mathf.Min(scaleX, scaleY);

            imageRenderer.transform.localScale = new Vector3(finalScale, finalScale, 1f);
        }
    }

    void OnMouseDown()
    {
        if (gameManager != null)
        {
            gameManager.OnPopupClicked(this);
        }
    }
}
