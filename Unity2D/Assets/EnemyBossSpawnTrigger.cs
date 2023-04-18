using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossSpawnTrigger : MonoBehaviour
{
    public GameObject boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!boss.activeInHierarchy)
            {
                boss.SetActive(true);
            }
        }
    }
}
