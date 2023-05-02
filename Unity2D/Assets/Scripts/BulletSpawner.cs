/*
 * BulletSpawner.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script controls the spawning of the collectible bullets in Level 4.
 * 
 * It has an array of bulletPrefabs that contains different types of bullets, 
 * an array of spawnPoints that determine where the bullets will spawn from, 
 * and a spawnDelay variable that determines how often the bullets will spawn.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject[] bulletPrefabs;
    public Transform[] spawnPoints;
    public float spawnDelay = 3f;

    private int lastSpawnIndex = -1;
    private GameObject currentBullet;

    private void Start()
    {
        SpawnBullet();
    }

    private void SpawnBullet()
    {
        if (!PlayerVisibility.GetInvisible() || currentBullet != null)
        {
            return;
        }

        // Randomly select a bullet prefab
        int bulletIndex = Random.Range(0, bulletPrefabs.Length);
        GameObject bulletPrefab = bulletPrefabs[bulletIndex];

        // Randomly select a spawn point, ensuring it's not the same as the last one
        int spawnIndex;
        do
        {
            spawnIndex = Random.Range(0, spawnPoints.Length);
        } while (spawnIndex == lastSpawnIndex);

        lastSpawnIndex = spawnIndex;
        Transform spawnPoint = spawnPoints[spawnIndex];

        // Instantiate the bullet prefab at the selected spawn point
        currentBullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    private IEnumerator SpawnAfterDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnBullet();
    }

    private void Update()
    {
        if (currentBullet == null && PlayerVisibility.GetInvisible())
        {
            SpawnBullet();
        }

        if (currentBullet != null)
        {
            Collider2D[] overlappingColliders = Physics2D.OverlapCircleAll(currentBullet.transform.position, 1f);

            foreach (Collider2D collider in overlappingColliders)
            {
                if (collider.CompareTag("Player"))
                {
                    // Set the currentBullet to null after the player collects it
                    currentBullet = null;

                    // Spawn a new bullet after the delay
                    StartCoroutine(SpawnAfterDelay());

                    break;
                }
            }
        }
    }
}
