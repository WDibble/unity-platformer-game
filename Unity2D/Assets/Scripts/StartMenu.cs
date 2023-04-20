using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Reference to the Timer script
    private Timer timer;

    private void Start()
    {
        // Find the Timer object and get its Timer script component
        timer = FindObjectOfType<Timer>();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenu()
    {
        Destroy(timer.gameObject); // Destroy Timer as a new one is created in the Start Scene
        SceneManager.LoadScene("Start Scene"); // Load the main menu scene
    }
}
