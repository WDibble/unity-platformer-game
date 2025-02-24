/*
 * CreditsMenu.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script is responsible for controlling the Credits menu in the game.
 * It has a reference to the UI game object of the Credits menu, which can be toggled on and off by the player.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour
{
    public GameObject creditsMenuUI;

    private bool isPaused = false;

    // Reference to the Timer script
    private Timer timer;

    private LevelManager levelManager;

    private AudioManager audioManager;

    private void Start()
    {
        // Find the Timer object and get its Timer script component
        timer = FindObjectOfType<Timer>();

        levelManager = FindObjectOfType<LevelManager>();

        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Resume the game and deactivate the pause menu
    public void Resume()
    {
        creditsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Credits()
    {
            // Toggle the pause menu based on the current state
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        audioManager.PlaySelect1Sound();
        audioManager.PlaySelect3Sound();
    }

    // Pause the game and activate the pause menu
    void Pause()
    {
        creditsMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    // Load the main menu scene
    public void MainMenu()
    {
        Time.timeScale = 1f; // Reset time scale to 1 before quitting
        Destroy(timer.gameObject); // Destroy Timer as a new one is created in the Start Scene
        SceneManager.LoadScene("Start Scene"); // Load the main menu scene

        audioManager.PlaySelect2Sound();
        audioManager.PlaySelect4Sound();
    }
}
