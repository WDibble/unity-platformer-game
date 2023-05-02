/*
 * JumpingEnemyAI.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script is for controlling the behavior of a jumping enemy in the game.
 * The enemy is designed to jump towards a target and damage the player on contact. 
 * The script uses pathfinding to calculate and update the enemy's path towards the target,
 * but also uses a jump trajectory instead of a path if the enemy is on the ground and within a certain distance from the target. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// Define JumpingEnemyAI class
public class JumpingEnemyAI : MonoBehaviour
{
    // Declare public variables
    public Transform target; // Target for the enemy to follow
    public Transform enemyGraphics; // Transform for the enemy's sprite
    public float jumpDelay = 2f; // Time delay between jumps
    public float jumpPower; // Jump power for the enemy
    public int health = 100; // Health value of the enemy
    
    // Declare private variables   
    private Vector2 jumpStartPosition; // Starting position of the jump
    private Vector2 jumpTargetPosition; // Target position of the jump
    private Rigidbody2D rb; // Rigidbody2D component of the enemy
    private Seeker seeker; // Seeker component for pathfinding
    private BoxCollider2D collision; // BoxCollider2D component of the enemy
    private SpriteRenderer sprite; // SpriteRenderer component of the enemy
    private float distance; // Distance between the enemy and the target
    private AudioManager audioManager; // AudioManager component for playing sounds

    [SerializeField] private LayerMask onGroundMask; // LayerMask to check if the enemy is grounded

    // Declare public float variable
    public float nextWaypointDistance = 3f; // Distance to the next waypoint

    // Start is called before the first frame update
    void Start()
    {
        // Assign component values
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        collision = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        audioManager = FindObjectOfType<AudioManager>();

        // Repeatedly call UpdatePath function
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    // UpdatePath function calculates and updates enemy's path towards target
    void UpdatePath()
    {
        distance = Vector2.Distance(target.position, rb.position);
        // Check if seeker is done and enemy is on ground
        if (seeker.IsDone() && onGround() && !PlayerVisibility.GetInvisible() && distance < 10f)
        {
            // Calculate jump trajectory instead of path
            jumpStartPosition = rb.position;
            jumpTargetPosition = target.position;
            jumpTargetPosition.y += 10.5f; // jump above target position
            float gravity = Physics2D.gravity.y;
            float timeToReachTarget = Mathf.Sqrt(-2f * jumpTargetPosition.y / gravity) + Mathf.Sqrt(2f * jumpStartPosition.y / gravity);

            // Check if timeToReachTarget is valid
            if (timeToReachTarget > 0f)
            {
                // Calculate velocity and make enemy jump
                Vector2 velocity = (jumpTargetPosition - jumpStartPosition) * (timeToReachTarget);
                velocity.y = Mathf.Abs(velocity.y);
                if (velocity.x > 19)
                {
                    velocity.x = 19f;
                }
                else if (velocity.x < -19)
                {
                    velocity.x = -19f;
                }
                rb.velocity = velocity;
                audioManager.PlayBigJumpSound();
            }
            else // Handle invalid timeToReachTarget
            {
                Debug.LogWarning("Invalid timeToReachTarget: " + timeToReachTarget);
            }

            // Store the x position of the target
            float targetXPos = target.position.x;

            // Determine which way the enemy should face
            if (transform.position.x > targetXPos)
            {
                // Face left
                sprite.flipX = true;
            }
            else
            {
                // Face right
                sprite.flipX = false;
            }
        }
    }

    // Check if the enemy is on the ground
    private bool onGround()
    {
        return Physics2D.BoxCast(collision.bounds.center, collision.bounds.size, 0f, Vector2.down, .1f, onGroundMask);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        audioManager.PlayHit2Sound();

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        audioManager.PlayHit1Sound();
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            // Ignore collisions between the enemy and the trap object
            Physics2D.IgnoreCollision(collision, other, true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            // Restore collisions between the enemy and the trap object
            Physics2D.IgnoreCollision(collision, other, false);
        }
    }
}