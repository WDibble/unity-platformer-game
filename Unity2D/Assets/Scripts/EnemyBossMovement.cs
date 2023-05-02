/*
 * EnemyBossMovement.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script controls the movement and behavior of the enemy boss in Level 2.
 * The boss will jump towards the player character and attempt to catch up with them.
 * If the boss catches up to the player, the player will be killed.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject bossHead; // Reference to the Boss Head object
    public float jumpForce = 15f;
    public float jumpFrequency = 2f;
    public float horizontalSpeed = 1f;
    public float catchUpDistance = 0.5f;
    public float rotationSpeed = 2f; // Added rotation speed variable
    private Rigidbody2D rb;
    private readonly float minYPosition = -1.26f;
    private bool playerDead = false;
    private AudioManager audioManager;

    // Initialize components and start jump routine
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(JumpRoutine());
        audioManager = FindObjectOfType<AudioManager>();
    }

    // FixedUpdate is used for physics-related updates
    private void FixedUpdate()
    {
        // Check if the boss has caught up with the player
        PlayerLife playerLife = player.GetComponent<PlayerLife>();
        if (Mathf.Abs(transform.position.x - player.transform.position.x) <= catchUpDistance)
        {
            // If the player is not dead, kill the player
            if (!playerDead)
            {
                playerDead = true;
                playerLife.Die();
            }
        }

        // Limit the boss's vertical position
        if (transform.position.y < minYPosition)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            transform.position = new Vector2(transform.position.x, minYPosition);
        }
    }

    // Update the boss's head rotation to face the player
    private void Update()
    {
        // Make the Boss Head look at the player
        Vector3 direction = player.transform.position - bossHead.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        bossHead.transform.rotation = Quaternion.Lerp(bossHead.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Coroutine to handle the boss's jumping behavior
    private IEnumerator JumpRoutine()
    {
        while (true)
        {
            // Wait for a duration depending on the player's visibility state
            if (PlayerVisibility.IsInvisible)
            {
                yield return new WaitForSeconds(jumpFrequency * 0.75f);
            }
            else
            {
                yield return new WaitForSeconds(jumpFrequency);
            }
            // If the boss is on the ground, apply a force to make it jump
            if (transform.position.y <= minYPosition)
            {
                rb.velocity = new Vector2(0, 0); // Reset velocity before applying the force
                transform.position = new Vector2(transform.position.x, minYPosition);
                rb.AddForce(new Vector2(horizontalSpeed, jumpForce), ForceMode2D.Impulse);
                audioManager.PlayBigJumpSound();
            }
        }
    }
}
