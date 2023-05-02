/*
 * Bullet2.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script defines the behavior of the bullet object, including its speed, damage, 
 * and what happens when it collides with an enemy or other object.
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{

    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private int damage = 25;
    private Rigidbody2D rb;
    [SerializeField] private GameObject bullet2Impact;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component and set the bullet's velocity
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
    }

    // OnTriggerEnter2D is called when the bullet enters a collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet is not colliding with the CameraConfiner object
        if (!collision.gameObject.CompareTag("CameraConfiner"))
        {
            // Check for different enemy types and apply damage accordingly
            JumpingEnemyAI JumpingEnemy = collision.GetComponent<JumpingEnemyAI>();
            RollingEnemyAI RollingEnemy = collision.GetComponent<RollingEnemyAI>();
            BatEnemyAI BatBossEnemy = collision.GetComponent<BatEnemyAI>();

            if (JumpingEnemy != null)
            {
                JumpingEnemy.TakeDamage(damage);
            }
            else if (RollingEnemy != null)
            {
                RollingEnemy.TakeDamage(damage);
            }
            else if (BatBossEnemy != null)
            {
                BatBossEnemy.TakeDamage(damage);
            }

            // Instantiate the bullet impact effect and destroy the bullet
            Instantiate(bullet2Impact, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
