/*
 * RollingEnemyAI.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script controls the AI behavior of the rolling enemy in the game.
 * The enemy follows a path to move towards the player's position, using the A* pathfinding algorithm implemented through the Pathfinding package.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RollingEnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    public int health = 100;

    Seeker seeker;
    Rigidbody2D rb;
    private SpriteRenderer sprite;
    private BoxCollider2D collision;
    private AudioManager audioManager;

    void Start()
    {
        // Get required components for AI navigation and audio
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        collision = GetComponent<BoxCollider2D>();
        audioManager = FindObjectOfType<AudioManager>();

        // Update the path periodically
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        // Calculate distance to target and determine maximum allowed distance
        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        float maxDistance = PlayerVisibility.GetInvisible() ? 8f : 13f;

        // If the target is too far away, don't update the path
        if (distanceToTarget > maxDistance)
        {
            return;
        }

        // Request a new path if the seeker is not currently calculating one
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        // Set the new path if it is valid
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        // If there's no path, don't do anything
        if (path == null)
        {
            return;
        }

        // Calculate distance to target and determine if the enemy should move towards it
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget > 13f)
        {
            if (PlayerVisibility.GetInvisible() && distanceToTarget > 8f)
            {
                return;
            }
        }

        // Move the enemy along the path
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        // Calculate direction and force to apply to the enemy
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = speed * Time.deltaTime * direction;

        // Halve the force if the player is invisible
        if (PlayerVisibility.GetInvisible())
        {
            force = (speed / 2) * Time.deltaTime * direction;
        }

        rb.AddForce(force);

        // Move to the next waypoint if close enough
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // Determine which way the enemy should face based on the target's x position
        if (transform.position.x > target.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }

    // Function to apply damage to the enemy
    public void TakeDamage(int damage)
    {
        health -= damage;

        // Play damage sound
        audioManager.PlayHit2Sound();

        // If the enemy's health reaches 0, call the Die function
        if (health <= 0)
        {
            Die();
        }
    }

    // Function to destroy the enemy when its health reaches 0
    void Die()
    {
        // Play death sound
        audioManager.PlayHit1Sound();
        // Destroy the enemy object
        Destroy(gameObject);
    }

    // Function to handle collisions with other objects using OnTriggerEnter2D
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            // Ignore collisions between the enemy and the trap object
            Physics2D.IgnoreCollision(collision, other, true);
        }
    }

    // Function to handle when the enemy stops colliding with other objects using OnTriggerExit2D
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            // Restore collisions between the enemy and the trap object
            Physics2D.IgnoreCollision(collision, other, false);
        }
    }

}