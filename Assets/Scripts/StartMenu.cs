/*
 * StartMenu.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script defines the behavior of the start menu in the game, including loading levels, resetting the time scale, and playing sounds.
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Reference to the Timer script
    private Timer timer;

    // Reference to the LevelManager script
    private LevelManager levelManager;

    // Reference to the AudioManager script
    private AudioManager audioManager;

    private void Start()
    {
        // Find the Timer object and get its Timer script component
        timer = FindObjectOfType<Timer>();
        // Find the LevelManager object and get its LevelManager script component
        levelManager = FindObjectOfType<LevelManager>();
        // Find the AudioManager object and get its AudioManager script component
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void StartGame()
    {
        // Load Level 1 when Start Game is clicked
        levelManager.LoadLevel("Level 1");
        // Play selection sounds
        audioManager.PlaySelect1Sound();
        audioManager.PlaySelect4Sound();
    }

    public void Tutorial()
    {
        // Load Level 0 (Tutorial) when Tutorial is clicked
        levelManager.LoadLevel("Level 0");
        // Play selection sounds
        audioManager.PlaySelect1Sound();
        audioManager.PlaySelect4Sound();
    }

    // Load the main menu scene
    public void MainMenu()
    {
        // Reset time scale to 1 before returning to the main menu
        Time.timeScale = 1f;

        // Destroy the Timer object if it exists
        if (timer != null)
        {
            Destroy(timer.gameObject);
        }

        // Load the main menu scene
        SceneManager.LoadScene("Start Scene");

        // Play selection sounds
        audioManager.PlaySelect1Sound();
        audioManager.PlaySelect4Sound();
    }

}
