using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // --- DATA ---
    private LevelSO currentLevelData;
    private List<QuestionData> questions;
    private int questionsAttempted = 0;
    private int correctAnswers = 0;
    private bool isGameOver = false;
    private bool isPaused = false;

    // --- GAME SETTINGS ---
    public float roundTime = 15f;
    private Coroutine currentTimer;

    // --- GAME OBJECTS ---
    public Transform[] spawnPositions;
    public GameObject popupPrefab;

    // --- MANAGER REFERENCES ---
    private UIManager uiManager;

    void Start()
    {
        // Load the level data that was selected in the previous scene.
        currentLevelData = LevelLoader.SelectedLevel;
        if (currentLevelData == null)
        {
            // Stop the Start() function to prevent further errors.
            Debug.LogError("FATAL ERROR: No Level Data was found! Check that the level selection button is connected properly. Returning to menu.");
            return;
        }

        // Copy the questions from the Scriptable Object into a new list for this level.
        questions = new List<QuestionData>(currentLevelData.questions); 
        uiManager = FindObjectOfType<UIManager>();
        // To display level name and set the initial score display from loaded data
        uiManager.UpdateLevelText(currentLevelData.levelName); 
        uiManager.UpdateScoreText(correctAnswers, questions.Count);
        StartNewRound();
    }

    // --- BUTTON FUNCTIONS ---
    // This function for the Pause and Resume buttons.
    public void TogglePause()
    {
        isPaused = !isPaused;
        uiManager.TogglePauseMenu(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    // This function is called by the "Back to Levels" button in the pause menu.
    public void BackToLevelSelection()
    {
        // IMPORTANT: Always reset time scale to normal before leaving a paused scene 
        // to avoid issues in the next scene.
        // Chcek the game mode and load the appropriate level selection scene.
        Time.timeScale = 1f;

        if (currentLevelData.gameMode == GameMode.ChooseWord)
        {
            SceneManager.LoadScene("WordsLevelsSelection");
        }
        else if (currentLevelData.gameMode == GameMode.ChoosePicture)
        {
            SceneManager.LoadScene("PicturesLevelsSelection");
        }
    }

    // This function is for the "Retry" buttons.
    public void RetryLevel()
    {
        // Unfreeze time before reloading the scene.
        Time.timeScale = 1f;
        SceneManager.LoadScene("Levels");
    }

    // --- GAME LOGIC ---
    // This function is called when all questions have been attempted.
    void EndLevel()
    {
        isGameOver = true;
        if (currentTimer != null) StopCoroutine(currentTimer);
        GameData.FinalScore = correctAnswers + " / " + questions.Count;
        // This is my winning condition of the game
        // (e.g. 5 questions - 2 = must have at least 3 questions answered correctly to win).
        int requiredScore = questions.Count - 2;
        GameData.PlayerWon = (correctAnswers >= requiredScore);
        GameData.LastPlayedMode = currentLevelData.gameMode;
        // Load the EndScene to display the results.
        SceneManager.LoadScene("EndScene");
    }

    // This function sets up and starts a new round of questions.
    // It checks if the game is over or if all questions have been attempted.
    // Control the popup prefab state and position.
    // I added Debug.Log statements to help track the state of the popup prefab and its position.
    // Because I was having issues with the prefab not being active when it was instantiated. 
    void StartNewRound()
    {
        if (isGameOver) return;
        if (questionsAttempted >= questions.Count) { EndLevel(); return; }

        // Debug prefab state
        Debug.Log("Popup Prefab Active: " + popupPrefab.activeSelf);

        if (currentTimer != null) StopCoroutine(currentTimer);
        currentTimer = StartCoroutine(TimerRoutine());

        QuestionData currentQuestion = questions[questionsAttempted];

        if (currentLevelData.gameMode == GameMode.ChooseWord)
        {
            uiManager.ShowImagePrompt(currentQuestion.correctImage);
            List<Transform> availablePositions = new List<Transform>(spawnPositions);
            List<string> availableWords = new List<string>(currentLevelData.distractorWords);

            // Spawn Correct Word Popups
            GameObject correctPopup = Instantiate(popupPrefab, GetRandomPosition(ref availablePositions).position, Quaternion.identity);
            correctPopup.tag = "Popup";
            correctPopup.SetActive(true); // Ensure popup is active
            Popup popupScript = correctPopup.GetComponent<Popup>();
            popupScript.SetupAsWord(currentQuestion.word, true);
            Debug.Log($"Correct Popup Active: {correctPopup.activeSelf}, Position: {correctPopup.transform.position}, Text: {currentQuestion.word}");

            // Spawn Incorrect Words Popups
            for (int i = 0; i < 2; i++)
            {
                GameObject wrongPopup = Instantiate(popupPrefab, GetRandomPosition(ref availablePositions).position, Quaternion.identity);
                wrongPopup.tag = "Popup";
                wrongPopup.SetActive(true); // Ensure popup is active
                string wrongWord = GetRandomWord(ref availableWords);
                wrongPopup.GetComponent<Popup>().SetupAsWord(wrongWord, false);
                Debug.Log($"Wrong Popup Active: {wrongPopup.activeSelf}, Position: {wrongPopup.transform.position}, Text: {wrongWord}");
            }
        }
        else if (currentLevelData.gameMode == GameMode.ChoosePicture)
        {
            uiManager.ShowTextPrompt(currentQuestion.word);
            List<Transform> availablePositions = new List<Transform>(spawnPositions);
            List<Sprite> availableImages = new List<Sprite>(currentLevelData.distractorImages);

            // Spawn Correct Picture Popups
            GameObject correctPopup = Instantiate(popupPrefab, GetRandomPosition(ref availablePositions).position, Quaternion.identity);
            correctPopup.tag = "Popup";
            correctPopup.SetActive(true); // Ensure popup is active
            Popup popupScript = correctPopup.GetComponent<Popup>();
            popupScript.SetupAsPicture(currentQuestion.correctImage, true);
            Debug.Log($"Correct Popup Active: {correctPopup.activeSelf}, Position: {correctPopup.transform.position}, Sprite: {currentQuestion.correctImage?.name}");

            // Spawn Incorrect Picture Popups
            for (int i = 0; i < 2; i++)
            {
                GameObject wrongPopup = Instantiate(popupPrefab, GetRandomPosition(ref availablePositions).position, Quaternion.identity);
                wrongPopup.tag = "Popup";
                wrongPopup.SetActive(true); // Ensure popup is active
                Sprite wrongImage = GetRandomImage(ref availableImages);
                wrongPopup.GetComponent<Popup>().SetupAsPicture(wrongImage, false);
                Debug.Log($"Wrong Popup Active: {wrongPopup.activeSelf}, Position: {wrongPopup.transform.position}, Sprite: {wrongImage?.name}");
            }
        }
    }

    // This helper function gets a random image from a list and removes it to prevent duplicates in the same round.
    private Sprite GetRandomImage(ref List<Sprite> images)
    {
        int index = Random.Range(0, images.Count);
        Sprite img = images[index];
        images.RemoveAt(index);
        return img;
    }

    // This function is called by the Popup script when a pop-up is clicked.
    // It checks if the game is over or paused, and if not, it processes the click.
    // If the clicked pop-up is correct, it updates the score, plays a correctsound and starts the next round.
    // If the clicked pop-up is incorrect, it plays a wrongsound and starts the next round with feedback text.
    public void OnPopupClicked(Popup clickedPopup)
    {
        if (isGameOver || isPaused) return;

        if (clickedPopup.isCorrect)
        {
            SoundManager.Instance.PlayCorrectSound();
            correctAnswers++;
            uiManager.UpdateScoreText(correctAnswers, questions.Count);
            StartCoroutine(NextRoundRoutine(null));
        }
        else
        {
            SoundManager.Instance.PlayWrongSound();
            StartCoroutine(NextRoundRoutine("Oops! Wrong one."));
        }
    }

    // This coroutine handles the countdown timer for each round.
    IEnumerator TimerRoutine()
    {
        float timer = roundTime;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            uiManager.UpdateTimerText(timer);
            yield return null;
        }
        StartCoroutine(NextRoundRoutine("Time's up!"));
    }

    // This coroutine handles the transition between rounds
    // Destroying old pop-ups tagged with "Popup" tag , showing feedback).
    IEnumerator NextRoundRoutine(string feedbackMessage)
    {
        questionsAttempted++;
        if (currentTimer != null) StopCoroutine(currentTimer);

        GameObject[] popupsToDestroy = GameObject.FindGameObjectsWithTag("Popup");
        Debug.Log($"Destroying {popupsToDestroy.Length} popups");
        foreach (var p in popupsToDestroy)
        {
            Debug.Log($"Destroying Popup: {p.name}");
            Destroy(p);
        }

        if (feedbackMessage != null)
        {
            uiManager.UpdatePromptImage(null);
            uiManager.SetFeedbackText(feedbackMessage, true);
            yield return new WaitForSeconds(1.5f);
            uiManager.SetFeedbackText("", false);
        }

        // Wait for the end of the frame to ensure all objects are destroyed before creating new ones.
        yield return new WaitForEndOfFrame();
        // If the game hasn't ended, start the next round.
        if (!isGameOver)
        {
            StartNewRound();
        }
    }

    // This helper function gets a random position from a list and removes it to prevent duplicates.
    private Transform GetRandomPosition(ref List<Transform> positions)
    {
        int index = Random.Range(0, positions.Count);
        Transform pos = positions[index];
        positions.RemoveAt(index);
        return pos;
    }

    // This helper function gets a random word from a list and removes it to prevent duplicates.
    private string GetRandomWord(ref List<string> words)
    {
        int index = Random.Range(0, words.Count);
        string word = words[index];
        words.RemoveAt(index);
        return word;
    }
}