using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float height = 3f;

    private float tntDropDelay = 3f;
    private float lastTntDropTime;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    private Seeker seeker;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private BoxCollider2D collision;

    [SerializeField] private Transform dropPoint;
    [SerializeField] private GameObject TNTPrefab;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        collision = GetComponent<BoxCollider2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void DropTNT()
    {
        Instantiate(TNTPrefab, dropPoint.position, dropPoint.rotation);

        if (PlayerVisibility.GetInvisible())
        {
            // Spawn two additional TNTs
            Instantiate(TNTPrefab, dropPoint.position + Vector3.left * 1.25f + Vector3.up * 0.33f, dropPoint.rotation);
            Instantiate(TNTPrefab, dropPoint.position + Vector3.right * 1.25f + Vector3.up * 0.33f, dropPoint.rotation);
        }
    }

    void UpdatePath()
    {
        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        float maxDistance = PlayerVisibility.GetInvisible() ? 8f : 13f;

        if (distanceToTarget > maxDistance)
        {
            // Player is too far away, don't update the path
            return;
        }

        if (seeker.IsDone())
        {
            // Calculate the modified target position
            Vector3 modifiedTargetPosition = target.position + Vector3.up * height + Vector3.right * 1f;

            // Start the pathfinding algorithm with the modified target position
            seeker.StartPath(rb.position, modifiedTargetPosition, OnPathComplete);
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
