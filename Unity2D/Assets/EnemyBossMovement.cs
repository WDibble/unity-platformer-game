using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject bossHead; // Reference to the Boss Head object
    public float jumpForce = 15f;
    public float jumpFrequency = 2f;
    public float horizontalSpeed = 1f;
    public float catchUpDistance = 0.5f;
    public float rotationSpeed = 2f; // Added rotation speed variable
    private Rigidbody2D rb;
    private readonly float minYPosition = -1.26f;
    private bool playerDead = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(JumpRoutine());
    }

    private void FixedUpdate()
    {    
        PlayerLife playerLife = player.GetComponent<PlayerLife>();

        if (Mathf.Abs(transform.position.x - player.transform.position.x) <= catchUpDistance)
        {
            if (!playerDead)
            {
                playerDead = true;
                playerLife.Die();
            }           
        }

        if (transform.position.y < minYPosition)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            transform.position = new Vector2(transform.position.x, minYPosition);
        }
    }

    private void Update()
    {
        // Make the Boss Head look at the player
        Vector3 direction = player.transform.position - bossHead.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        bossHead.transform.rotation = Quaternion.Lerp(bossHead.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private IEnumerator JumpRoutine()
    {
        while (true)
        {
            if (PlayerVisibility.IsInvisible)
            {
                yield return new WaitForSeconds(jumpFrequency * 0.75f);
            }
            else
            {
                yield return new WaitForSeconds(jumpFrequency);
            }
            if (transform.position.y <= minYPosition)
            {
                rb.velocity = new Vector2(0, 0); // Reset velocity before applying the force
                transform.position = new Vector2(transform.position.x, minYPosition);
                rb.AddForce(new Vector2(horizontalSpeed, jumpForce), ForceMode2D.Impulse);
            }
        }
    }
}
