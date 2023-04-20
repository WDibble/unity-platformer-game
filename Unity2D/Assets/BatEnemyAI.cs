using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BatEnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float verticalMovementSpeed = 0.5f; // Added variable for vertical movement

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    public int health = 100;

    private LevelManager levelManager;

    Seeker seeker;
    Rigidbody2D rb;
    private SpriteRenderer sprite;
    private BoxCollider2D collision;

    Material material;

    private bool batInvisible = false;
    private float fade = 1f;
    private float invisibilityDelay = 1f;
    private float timeToToggleInvisibility;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        collision = GetComponent<BoxCollider2D>();

        levelManager = FindObjectOfType<LevelManager>();

        // Get the material component of the bat
        material = GetComponent<SpriteRenderer>().material;

        // Repeatedly update the path to the target
        InvokeRepeating("UpdatePath", 0f, .5f);
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

        // Handle invisibility
        HandleInvisibility();

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
        if (PlayerVisibility.GetInvisible() != batInvisible)
        {
            if (timeToToggleInvisibility <= Time.time)
            {
                ToggleInvisibility();
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

        if (health <= 0)
        {
            Die();
            levelManager.LoadLevel("Final Score Screen");
        }
    }

    void Die()
    {
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

    private void ToggleInvisibility()
    {
        if (PlayerVisibility.GetInvisible() != batInvisible)
        {
            float targetFade = batInvisible ? 1f : 0f;
            fade = Mathf.MoveTowards(fade, targetFade, Time.deltaTime);

            if (Mathf.Approximately(fade, targetFade))
            {
                batInvisible = !batInvisible;
                timeToToggleInvisibility = Time.time + invisibilityDelay;
            }
            material.SetFloat("_Fade", fade);
        }
    }
}