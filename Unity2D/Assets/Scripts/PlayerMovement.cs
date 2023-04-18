using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D collision;
    private SpriteRenderer sprite;
    private Animator anim;

    private bool canDoubleJump = false;
    private bool hasDoubleJumped = false;

    [SerializeField] private LayerMask onGroundMask;
    [SerializeField] private GameObject teleportPoint;

    private float dirX = 0f;
    [SerializeField] private float moveVelocity = 7f;
    [SerializeField] private float jumpVelocity = 14f;

    private enum AnimationState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        collision = GetComponent<BoxCollider2D>();

        if (SceneManager.GetActiveScene().name == "Level 3")
        {
            canDoubleJump = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveVelocity, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            if (onGround())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
                jumpSound.Play();
                hasDoubleJumped = false;
            }
            else if (canDoubleJump && !hasDoubleJumped)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
                jumpSound.Play();
                hasDoubleJumped = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = teleportPoint.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            transform.position = new Vector2(120.5f, 0.7f);
        }

        UpdateAnimations();
    }


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

        if (rb.velocity.y > .1f)
        {
            state = AnimationState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = AnimationState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool onGround()
    {
        return Physics2D.BoxCast(collision.bounds.center, collision.bounds.size, 0f, Vector2.down, .1f, onGroundMask);
    }
}