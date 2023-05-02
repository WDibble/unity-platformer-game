/*
 * PlayerLife.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script handles the player's life, including detecting collisions with traps, handling the player's death, and restarting the level.
 * It also sets the player's bullet counts based on the current level.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D collide;

    // Reference to the Timer script
    private Timer timer;

    // Reference to the Audio Manager script
    private AudioManager audioManager;

    private LevelManager levelManager;

    // Invincibility toggle for debugging purposes
    [SerializeField] private bool invincible = false;

    private void Start()
    {
        // Get the necessary components from the player object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collide = GetComponent<BoxCollider2D>();

        // Find the Timer object and get its Timer script component
        timer = FindObjectOfType<Timer>();

        // Find the Audio object and get its Audio Manager script component
        audioManager = FindObjectOfType<AudioManager>();

        levelManager = FindObjectOfType<LevelManager>();

        // Set bullet counts based on the current level
        if (SceneManager.GetActiveScene().name == "Level 0")
        {
            timer.SetBullet1Count(0);
            timer.SetBullet2Count(0);
            timer.SetBullet3Count(0);
        }
        else if (SceneManager.GetActiveScene().name == "Level 1")
        {
            timer.SetBullet1Count(timer.GetBulletLevel1Count()[0]);
            timer.SetBullet2Count(timer.GetBulletLevel1Count()[1]);
            timer.SetBullet3Count(timer.GetBulletLevel1Count()[2]);
        }
        else if (SceneManager.GetActiveScene().name == "Level 2")
        {
            timer.SetBullet1Count(timer.GetBulletLevel2Count()[0]);
            timer.SetBullet2Count(timer.GetBulletLevel2Count()[1]);
            timer.SetBullet3Count(timer.GetBulletLevel2Count()[2]);
        }
        else if (SceneManager.GetActiveScene().name == "Level 3")
        {
            timer.SetBullet1Count(timer.GetBulletLevel3Count()[0]);
            timer.SetBullet2Count(timer.GetBulletLevel3Count()[1]);
            timer.SetBullet3Count(timer.GetBulletLevel3Count()[2]);
        }
        else if (SceneManager.GetActiveScene().name == "Level 4")
        {
            timer.SetBullet1Count(timer.GetBulletLevel4Count()[0]);
            timer.SetBullet2Count(timer.GetBulletLevel4Count()[1]);
            timer.SetBullet3Count(timer.GetBulletLevel4Count()[2]);
        }
    }

    // Detect collision with traps
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!invincible && collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    // Handle player death
    public void Die()
    {
        // Play the death sound
        audioManager.PlayHit3Sound();

        // Make the player's Rigidbody static so they don't move
        body.bodyType = RigidbodyType2D.Static;

        // Trigger the death animation
        anim.SetTrigger("death");

        // Disable the player's BoxCollider
        collide.enabled = false;

        // Make the player visible if they were invisible
        if (PlayerVisibility.IsInvisible)
        {
            PlayerVisibility.SetInvisible(false);
        }
    }

    // Restart the level after death
    private void LevelRestart()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Re-enable the player's BoxCollider
        collide.enabled = true;
    }
}
