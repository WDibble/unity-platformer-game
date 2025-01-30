/*
 * NextLevelTrigger.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script is attached to a trigger collider that loads the next level when the player enters it.
 * 
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    private LevelManager levelManager;

    // Reference to the Timer script
    private Timer timer;

    private void Start()
    {
        // Find the LevelManager object and get its LevelManager script component
        levelManager = FindObjectOfType<LevelManager>();

        // Find the Timer object and get its Timer script component
        timer = FindObjectOfType<Timer>();
    }

    // OnTriggerEnter2D is called when the player enters the trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Save the current bullet counts to the appropriate level's bullet count
            if (SceneManager.GetActiveScene().name == "Level 1")
            {
                int[] bulletCounts = { timer.GetBullet1Count(), timer.GetBullet2Count(), timer.GetBullet3Count() };
                timer.SetBulletLevel2Count(bulletCounts);
            }

            else if (SceneManager.GetActiveScene().name == "Level 2")
            {
                int[] bulletCounts = { timer.GetBullet1Count(), timer.GetBullet2Count(), timer.GetBullet3Count() };
                timer.SetBulletLevel3Count(bulletCounts);
            }

            else if (SceneManager.GetActiveScene().name == "Level 3")
            {
                int[] bulletCounts = { timer.GetBullet1Count(), timer.GetBullet2Count(), timer.GetBullet3Count() };
                timer.SetBulletLevel4Count(bulletCounts);
            }

            levelManager.LoadLevel(nextLevelName);
        }
    }
}
