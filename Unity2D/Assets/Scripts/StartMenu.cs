using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Reference to the Timer script
    private Timer timer;

    private LevelManager levelManager;

    private void Start()
    {
        // Find the Timer object and get its Timer script component
        timer = FindObjectOfType<Timer>();

        levelManager = FindObjectOfType<LevelManager>();
    }

    public void StartGame()
    {
        levelManager.LoadLevel("Level 1");
    }

    public void Tutorial()
    {
        levelManager.LoadLevel("Level 0");
    }

    public void MainMenu()
    {
        Destroy(timer.gameObject); // Destroy Timer as a new one is created in the Start Scene
        levelManager.LoadLevel("Start Scene");
    }
}
