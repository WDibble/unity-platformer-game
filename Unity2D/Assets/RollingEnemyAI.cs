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

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        collision = GetComponent<BoxCollider2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);       
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }       
    }

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

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = speed * Time.deltaTime * direction;

        if (PlayerVisibility.GetInvisible())
        {
            force = (speed/2) * Time.deltaTime * direction;
        }

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
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

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
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
}