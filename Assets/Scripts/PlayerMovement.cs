/*
 * PlayerMovement.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script is responsible for controlling the movement of the player character, including horizontal movement, jumping, and double jumping.
 * The script also updates the player's animations based on their movement and state.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Components and variables needed for player movement and attributes
    private Rigidbody2D rb;
    private BoxCollider2D collision;
    private Animator anim;

    // Reference to the Timer script
    private Timer timer;

    private LevelManager levelManager;

    // Reference to the Audio Manager script
    private AudioManager audioManager;

    // Variables for double jump
    private bool canDoubleJump = false;
    private bool hasDoubleJumped = false;

    // Variables for ground detection
    [SerializeField] private LayerMask onGroundMask;
    [SerializeField] private GameObject teleportPoint;

    private float dirX = 0f;
    [SerializeField] private float moveVelocity = 7f;
    [SerializeField] private float jumpVelocity = 14f;

    // Animation states for the player
    private enum AnimationState { idle, running, jumping, falling }

    // Start is called before the first frame update
    void Start()
    {
        // Get the necessary components
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collision = GetComponent<BoxCollider2D>();

        // Find the Timer object and get its Timer script component
        timer = FindObjectOfType<Timer>();

        // Get the LevelManager script
        levelManager = FindObjectOfType<LevelManager>();

        // Find the Audio object and get its Audio Manager script component
        audioManager = FindObjectOfType<AudioManager>();

        // Enable double jump in certain levels
        if (SceneManager.GetActiveScene().name == "Level 3")
        {
            canDoubleJump = true;
        }

        else if (SceneManager.GetActiveScene().name == "Level 4")
        {
            canDoubleJump = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get the input for horizontal movement
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveVelocity, rb.velocity.y);

        // Handle jumping and double jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (onGround())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
                audioManager.PlayJump2Sound();
                hasDoubleJumped = false;
            }
            else if (canDoubleJump && !hasDoubleJumped)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
                audioManager.PlayJump2Sound();
                hasDoubleJumped = true;
            }
        }

        /*// Teleport the player to the teleport point when 'R' is pressed (DEV DEBUG USE ONLY)
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = teleportPoint.transform.position;
        }*/


        // FOR DEMO PURPOSES, PRESSING 1, 2, 3, OR 4, WILL QUICK JUMP YOU TO WHICHEVER LEVEL IS CHOSEN.
        // THIS FEATURE WOULD NOT BE INCLUDED IN THE PUBLISHED BUILD AND IS INTENDED TO BE USED BY THE DEVELOPER OR WHEN BEING ASSESSED.
        // Skip to Level 1 and set bullet counts when '1' is pressed
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            int[] bulletCounts = { 8, 8, 8 };
            timer.SetBulletLevel1Count(bulletCounts);
            levelManager.LoadLevel("Level 1");
        }

        // Skip to Level 2 and set bullet counts when '2' is pressed
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            int[] bulletCounts = { 8, 8, 8 };
            timer.SetBulletLevel2Count(bulletCounts);
            levelManager.LoadLevel("Level 2");
        }

        // Skip to Level 3 and set bullet counts when '3' is pressed
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            int[] bulletCounts = { 8, 8, 8 };
            timer.SetBulletLevel3Count(bulletCounts);
            levelManager.LoadLevel("Level 3");
        }

        // Skip to Level 4 and set bullet counts when '4' is pressed
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            int[] bulletCounts = { 8, 8, 8 };
            timer.SetBulletLevel4Count(bulletCounts);
            levelManager.LoadLevel("Level 4");
        }

        // Update the player's animations based on their movement
        UpdateAnimations();
    }

    // Function to update the player's animations based on their movement and state
    private void UpdateAnimations()
    {
        AnimationState state;

        if (dirX > 0f)
        {
            state = AnimationState.running;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (dirX < 0f)
        {
            state = AnimationState.running;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            state = AnimationState.idle;

        }

        // Set the animation state based on the player's vertical movement
        if (rb.velocity.y > .1f)
        {
            state = AnimationState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = AnimationState.falling;
        }

        // Update the animation state in the Animator component
        anim.SetInteger("state", (int)state);
    }

    // Function to check if the player is on the ground
    private bool onGround()
    {
        // Use a BoxCast to check for ground collision, returns true if on ground
        return Physics2D.BoxCast(collision.bounds.center, collision.bounds.size, 0f, Vector2.down, .1f, onGroundMask);
    }
}