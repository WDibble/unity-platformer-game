/*
 * LevelManager.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script manages the loading of levels and transition effects between them.
 * 
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Timer timer; // Reference to the Timer script
    public TransitionImageController transitionImageController; // Reference to the TransitionImageController script

    private void Start()
    {
        // Add a delegate method to be called when a new scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Remove the delegate method when the object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This method is called when a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Start the timer when Level 1 is loaded
        if (scene.name == "Level 1")
        {
            timer.StartTimer();
        }

        // Trigger the fade-out effect when the scene loads
        transitionImageController.FadeOut();
    }

    // Load the specified level with a transition effect
    public void LoadLevel(string levelName)
    {
        // Trigger the fade-in effect and wait for it to complete before loading a new scene
        StartCoroutine(LoadLevelWithTransition(levelName));
    }

    // Coroutine to load the level with a fade-in transition
    private IEnumerator LoadLevelWithTransition(string levelName)
    {
        // Start the fade-in transition
        transitionImageController.FadeIn();

        // Wait for the transition to complete
        yield return new WaitForSeconds(transitionImageController.transitionTime);

        // Load the specified level
        SceneManager.LoadScene(levelName);
    }

    // Stop the timer
    public void StopTimer()
    {
        timer.StopTimer();
    }

}
