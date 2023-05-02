/*
 * FlyingEnemyAI.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script handles the behavior of a flying enemy AI in the game.
 * The enemy follows a target (the player) and drops TNT on them after a delay.
 * The script includes methods for updating the enemy's path, dropping TNT, handling collisions with traps, and flipping the sprite based on the direction of movement. 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemyAI : MonoBehaviour
{
    public Transform target; // Target (the player) that the enemy will follow

    public float speed = 200f; // Enemy movement speed
    public float nextWaypointDistance = 3f; // Distance to the next waypoint before switching to the next
    public float height = 3f; // Height above the target that the enemy will fly

    private float tntDropDelay = 3f; // Delay between dropping TNT
    private float lastTntDropTime; // Time the last TNT was dropped

    Path path; // Path the enemy will follow
    int currentWaypoint = 0; // Current waypoint along the path
    bool reachedEndOfPath = false; // Whether the enemy has reached the end of the path

    private Seeker seeker; // Seeker component for pathfinding
    private Rigidbody2D rb; // Rigidbody2D component for physics
    private SpriteRenderer sprite; // SpriteRenderer component for rendering and flipping sprites
    private BoxCollider2D collision; // BoxCollider2D component for collision detection
    private AudioManager audioManager; // AudioManager to play sound effects

    [SerializeField] private Transform dropPoint; // Transform where the TNT will be dropped
    [SerializeField] private GameObject TNTPrefab; // TNT prefab to be instantiated

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        collision = GetComponent<BoxCollider2D>();
        audioManager = FindObjectOfType<AudioManager>();

        // Repeatedly update the path every 0.5 seconds
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    // Function to instantiate and drop the TNT
    void DropTNT()
    {
        Instantiate(TNTPrefab, dropPoint.position, dropPoint.rotation);

        // If the player is invisible, spawn two additional TNTs
        if (PlayerVisibility.GetInvisible())
        {
            Instantiate(TNTPrefab, dropPoint.position + Vector3.left * 1.25f + Vector3.up * 0.33f, dropPoint.rotation);
            Instantiate(TNTPrefab, dropPoint.position + Vector3.right * 1.25f + Vector3.up * 0.33f, dropPoint.rotation);
        }
        audioManager.PlayMachineSound(); // Play sound effect when dropping TNT
    }

    // Function to update the enemy's path
    void UpdatePath()
    {
        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        float maxDistance = PlayerVisibility.GetInvisible() ? 8f : 13f;

        // If the player is too far away, don't update the path
        if (distanceToTarget > maxDistance)
        {
            return;
        }

        // If the pathfinding calculation is complete, start a new path to the modified target position
        if (seeker.IsDone())
        {
            Vector3 modifiedTargetPosition = target.position + Vector3.up * height + Vector3.right * 1f;
            seeker.StartPath(rb.position, modifiedTargetPosition, OnPathComplete);
        }
    }

    // Function to handle path completion
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        // If the player is out of range, do not move towards them
        if (distanceToTarget > 13f)
        {
            if (PlayerVisibility.GetInvisible() && distanceToTarget > 8f)
            {
                // Player is out of range, do not move towards them
                return;
            }
        }

        // If the enemy has reached the end of the path, stop moving
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // Check if enemy X position matches player X position
        if (Mathf.Abs(transform.position.x - target.position.x) < 0.5f)
        {
            // Check if the delay time for dropping TNT has elapsed
            if (Time.time - lastTntDropTime > tntDropDelay)
            {
                // Drop TNT
                DropTNT();

                // Update last TNT drop time
                lastTntDropTime = Time.time;
            }
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
