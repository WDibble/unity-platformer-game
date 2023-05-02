/*
 * EnemyBossSpawnTrigger.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script is attached to a trigger collider that activates the boss enemy for Level 2 when the player enters it.
 * 
 */

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
