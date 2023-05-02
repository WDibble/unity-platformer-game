/*
 * BatEnemyAI.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This is the script for controlling the AI of the final boss bat in Level 4.
 * 
 * The bat uses pathfinding to follow the player and has a health bar. 
 * 
 * It can also become invisible, and it moves at a slower speed when the player is invisible.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class BatEnemyAI : MonoBehaviour
{
    public Transform target; // Reference to the target (the player)

    public float speed = 200f; // Speed at which the bat moves towards the target
    public float nextWaypointDistance = 3f; // Distance before the bat moves to the next waypoint
    public float verticalMovementSpeed = 0.5f; // Speed for the bat's vertical movement
    private float originalHealthBarWidth;

    Path path; // The path for the bat to follow
    int currentWaypoint = 0; // Index of the current waypoint the bat is moving towards
    bool reachedEndOfPath = false; // Check if the bat has reached the end of the path
    public int health = 100; // Bat's health
    private int startingHealth = 0; // The bat's starting health

    private LevelManager levelManager; // Reference to the level manager script

    Seeker seeker; // Reference to the Seeker component (for pathfinding)
    Rigidbody2D rb; // Reference to the Rigidbody2D component
    private SpriteRenderer sprite; // Reference to the SpriteRenderer component
    private BoxCollider2D collision; // Reference to the BoxCollider2D component
    private AudioManager audioManager; // Reference to the AudioManager script

    [SerializeField] private Image healthBarImage; // Reference to the health bar UI image
    Material material; // Reference to the bat's material (for invisibility)

    private bool batInvisible = false; // Flag to track if the bat is invisible
    private float fade = 1f; // Opacity value for the bat's sprite (1 is fully visible, 0 is fully invisible)
    private float invisibilityDelay = 1f; // Delay before toggling the bat's invisibility
    private float timeToToggleInvisibility; // Time when the bat should toggle its invisibility
    private bool isChangingInvisibility = false; // Flag to track if the bat is in the process of changing its invisibility

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        collision = GetComponent<BoxCollider2D>();
        audioManager = FindObjectOfType<AudioManager>();

        levelManager = FindObjectOfType<LevelManager>();

        // Get the material component of the bat
        material = GetComponent<SpriteRenderer>().material;

        // Repeatedly update the path to the target
        InvokeRepeating("UpdatePath", 0f, .5f);

        originalHealthBarWidth = healthBarImage.GetComponent<RectTransform>().sizeDelta.x;

        startingHealth = health;
    }

    // Update the path to the target
    void UpdatePath()
    {
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    // Set the new path if it was found without errors
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void Update()
    {
        // Handle invisibility
        HandleInvisibility();
    }

    // FixedUpdate is called at a fixed interval and used for physics calculations
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget > 13f)
        {
            if (PlayerVisibility.GetInvisible() && distanceToTarget > 8f)
            {
                // Player is out of range, do not move towards them
                return;
            }
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        // Calculate the direction and force to move the bat towards the target
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = speed * Time.deltaTime * direction;

        if (PlayerVisibility.GetInvisible())
        {
            force = (speed / 2) * Time.deltaTime * direction;
        }

        rb.AddForce(force);

        // Update the current waypoint if the bat is close enough
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }


        // Determine which way the enemy should face
        float targetXPos = target.position.x;

        if (transform.position.x > targetXPos)
        {
            // Face left
            sprite.flipX = false;
        }
        else
        {
            // Face right
            sprite.flipX = true;
        }
    }

    // Handle the bat's behavior when the player is invisible
    private void HandleInvisibility()
    {
        if (!isChangingInvisibility && PlayerVisibility.GetInvisible() != batInvisible)
        {
            if (timeToToggleInvisibility <= Time.time)
            {
                StartCoroutine(ToggleInvisibilitySmoothly());
            }
        }
        else
        {
            timeToToggleInvisibility = Time.time + invisibilityDelay;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHealthBar();
        audioManager.PlayHit2Sound();

        if (health <= 0)
        {
            Die();
            levelManager.LoadLevel("Final Score Screen");
        }
    }

    private void UpdateHealthBar()
    {
        float healthPercentage = (float)health / startingHealth;
        RectTransform healthBarRectTransform = healthBarImage.GetComponent<RectTransform>();
        Vector2 sizeDelta = healthBarRectTransform.sizeDelta;
        sizeDelta.x = healthPercentage * originalHealthBarWidth;
        healthBarRectTransform.sizeDelta = sizeDelta;
    }

    void Die()
    {
        audioManager.PlayHit3Sound();
        audioManager.PlayExplosionSound();
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

    private IEnumerator ToggleInvisibilitySmoothly()
    {
        isChangingInvisibility = true;
        float targetFade = batInvisible ? 1f : 0f;

        while (!Mathf.Approximately(fade, targetFade))
        {
            fade = Mathf.MoveTowards(fade, targetFade, Time.deltaTime);
            material.SetFloat("_Fade", fade);
            yield return null;
        }

        batInvisible = !batInvisible;
        isChangingInvisibility = false;
    }
}