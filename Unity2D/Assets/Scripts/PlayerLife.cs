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

    [SerializeField] private AudioSource deathSound;

    private LevelManager levelManager;

    // Invincibility toggle for debugging purposes
    [SerializeField] private bool invincible = false;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collide = GetComponent<BoxCollider2D>();

        // Find the Timer object and get its Timer script component
        timer = FindObjectOfType<Timer>();

        levelManager = FindObjectOfType<LevelManager>();

        // Set bullet counts based on the current level
        if (SceneManager.GetActiveScene().name == "Level 1")
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
        deathSound.Play();
        body.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        collide.enabled = false;
        if (PlayerVisibility.IsInvisible)
        {
            PlayerVisibility.SetInvisible(false);
        }
    }

    // Restart the level after death
    private void LevelRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        collide.enabled = true;
    }
}
