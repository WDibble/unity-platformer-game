using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public Transform target;

    public float speed = 400f;
    public float nextWaypointDistance = 3f;
    public float attackDelay = 1f;

    public Transform enemyGraphics;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    bool isAttacking = false;
    float attackTimer = 0f;
    Vector2 facingDirection = Vector2.right;
    bool isMoving = false;

    Seeker seeker;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            Debug.LogError("No target object assigned to EnemyAI script on " + gameObject.name);
            enabled = false;
        }

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
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

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        // Check if it's time to update the current waypoint
        attackTimer -= Time.fixedDeltaTime;
        if (!isAttacking && attackTimer <= 0f)
        {
            attackTimer = attackDelay;
            if (Vector2.Distance(transform.position, target.position) <= nextWaypointDistance)
            {
                isAttacking = true;
                // Trigger attack animation or method here
            }
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            isMoving = false;
            return;
        }

        else
        {
            reachedEndOfPath = false;
            isMoving = true;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        // Update facing direction
        if (force.magnitude > 0f)
        {
            facingDirection = force.normalized;
        }

        // Flip graphics to face correct direction
        if (facingDirection.x < 0f)
        {
            enemyGraphics.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (facingDirection.x > 0f)
        {
            enemyGraphics.localScale = new Vector3(1f, 1f, 1f);
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // Update animator
        //animator.SetBool("IsMoving", isMoving);
    }
}
