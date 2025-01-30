/*
 * Timer.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script defines a Timer class with functionality for timing gameplay and managing bullet counts. 
 * It also has functions for starting and stopping the timer, and getters and setters for bullet counts and bullet level counts. 
 * Additionally, it has functions for subscribing to and unsubscribing from the SceneManager's sceneLoaded event, and a function for handling the event,
 * which starts the timer when the Level 1 scene is loaded and stops the timer when the Final Score Screen scene is loaded.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float elapsedTime = 0f;
    private bool isTiming = false;

    // Number of bullets for each type of bullet
    private int bullet1Count = 0;
    private int bullet2Count = 0;
    private int bullet3Count = 0;

    private int[] bulletLevel1 = { 0, 0, 0 };
    private int[] bulletLevel2 = { 0, 0, 0 };
    private int[] bulletLevel3 = { 0, 0, 0 };
    private int[] bulletLevel4 = { 0, 0, 0 };

    private void Awake()
    {
        // Make this game object persistent across scenes
        DontDestroyOnLoad(gameObject);
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene is Level 1
        if (scene.name == "Level 1")
        {
            // Start the timer
            StartTimer();
        }
        // Check if the loaded scene is the Final Score Screen
        else if (scene.name == "Final Score Screen")
        {
            // Stop the timer (commented out in the original script)
            // StopTimer();
        }
    }

    private void Update()
    {
        // If the timer is running, update the elapsed time
        if (isTiming)
        {
            elapsedTime += Time.deltaTime;
        }
    }

    public void StartTimer()
    {
        // Enable the timer
        isTiming = true;
    }

    public void StopTimer()
    {
        // Disable the timer
        isTiming = false;
    }

    public float GetTime()
    {
        // Return the current elapsed time
        return elapsedTime;
    }

    // Bullet count getters and setters
    public int GetBullet1Count()
    {
        return bullet1Count;
    }

    public void SetBullet1Count(int count)
    {
        bullet1Count = count;
    }

    public int GetBullet2Count()
    {
        return bullet2Count;
    }

    public void SetBullet2Count(int count)
    {
        bullet2Count = count;
    }

    public int GetBullet3Count()
    {
        return bullet3Count;
    }

    public void SetBullet3Count(int count)
    {
        bullet3Count = count;
    }

    // Bullet level count getters and setters
    public int[] GetBulletLevel1Count()
    {
        return bulletLevel1;
    }

    public void SetBulletLevel1Count(int[] count)
    {
        bulletLevel1 = count;
    }

    public int[] GetBulletLevel2Count()
    {
        return bulletLevel2;
    }

    public void SetBulletLevel2Count(int[] count)
    {
        bulletLevel2 = count;
    }

    public int[] GetBulletLevel3Count()
    {
        return bulletLevel3;
    }

    public void SetBulletLevel3Count(int[] count)
    {
        bulletLevel3 = count;
    }

    public int[] GetBulletLevel4Count()
    {
        return bulletLevel4;
    }

    public void SetBulletLevel4Count(int[] count)
    {
        bulletLevel4 = count;
    }
}
