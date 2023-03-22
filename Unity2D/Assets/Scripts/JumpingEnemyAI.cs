using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// Define JumpingEnemyAI class
public class JumpingEnemyAI : MonoBehaviour
{
    // Declare public variables
    public Transform target;
    public Transform enemyGraphics;
    public float jumpDelay = 2f;
    public float jumpPower;
    public int health = 100;
    // Declare private variables
    private float jumpForce;
    private bool isGrounded;
    private bool isJumping = false;
    private float jumpTimer = 0f;
    private Vector2 jumpStartPosition;
    private Vector2 jumpTargetPosition;
    private Vector2 facingDirection = Vector2.right;
    private Rigidbody2D rb;
    private Animator animator;
    private Seeker seeker;
    private Path path;
    private BoxCollider2D collision;
    private SpriteRenderer sprite;
    private float distance;

    [SerializeField] private LayerMask onGroundMask;

    // Declare integer variables
    private int currentWaypoint = 0;

    // Declare boolean variables
    private bool reachedEndOfPath = false;

    // Declare public float variable
    public float nextWaypointDistance = 3f;

    // Start is called before the first frame update
    void Start()
    {
        // Assign component values
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        seeker = GetComponent<Seeker>();
        collision = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

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
            }
            else  // Handle invalid timeToReachTarget
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

    public void TakeDamage (int damage)
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