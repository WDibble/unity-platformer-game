using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{

    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private int damage = 25;
    private Rigidbody2D rb;
    [SerializeField] private GameObject bullet1Impact;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("CameraConfiner"))
        {
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
            Instantiate(bullet1Impact, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
