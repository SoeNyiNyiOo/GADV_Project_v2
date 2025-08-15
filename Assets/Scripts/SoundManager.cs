using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    // Audio Sources
    private AudioSource sfxSource;      // For sound effects (PlayOneShot)
    private AudioSource bgmSource;      // For background music (Looping)

    // Sound Effect Clips
    public AudioClip buttonClickSound;
    public AudioClip correctPopupSound;
    public AudioClip wrongPopupSound;
    public AudioClip winSound;
    public AudioClip loseSound;

    // Background Music Clips
    public AudioClip menuBGM;           // For menus and other scenes
    public AudioClip gameBGM;           // For game level

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        // Get both AudioSource components
        AudioSource[] sources = GetComponents<AudioSource>();
        sfxSource = sources[0];
        bgmSource = sources[1];

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // This function is called every time a new scene is loaded. It checks the scene name.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Levels")
        {
            // Play the game BGM if the clip is wrong OR if the music is not currently playing
            if (bgmSource.clip != gameBGM || !bgmSource.isPlaying)
            {
                bgmSource.clip = gameBGM;
                bgmSource.Play();
            }
        }
        else if (scene.name == "EndScene")
        {
            // In the End Scene, stop all background music
            bgmSource.Stop();
        }
        else
        {
            // Play the menu BGM if the clip is wrong OR if the music is not currently playing
            if (bgmSource.clip != menuBGM || !bgmSource.isPlaying)
            {
                bgmSource.clip = menuBGM;
                bgmSource.Play();
            }
        }
    }

    // --- Sound Effect Functions ---
    // Note: These ones use sfxSource
   
    /*PlayButtonClickSound() is called in UIManager.cs
    PlayCorrectSound() is called in GameManager.cs
    PlayWrongSound() is called in GameManager.cs
    PlayWinSound() is called in EndSceneManager.cs
    PlayLoseSound()is called in EndSceneManager.cs
    Assigned SFX are not null they will play the SFX*/

    public void PlayButtonClickSound() { if (buttonClickSound != null) sfxSource.PlayOneShot(buttonClickSound); }
    public void PlayCorrectSound() { if (correctPopupSound != null) sfxSource.PlayOneShot(correctPopupSound); }
    public void PlayWrongSound() { if (wrongPopupSound != null) sfxSource.PlayOneShot(wrongPopupSound); }
    public void PlayWinSound() { if (winSound != null) sfxSource.PlayOneShot(winSound); }
    public void PlayLoseSound() { if (loseSound != null) sfxSource.PlayOneShot(loseSound); }
    public void StopSFX()
    {
        // This stops only the sound effects source
        sfxSource.Stop();
    }

    // To unsubscribe from events when the object is destroyed
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
