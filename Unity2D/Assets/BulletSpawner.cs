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
                    // Implement logic for the player to collect the bullet here
                    // ...

                    // Destroy the current bullet object
                    Destroy(currentBullet);
                    currentBullet = null;

                    // Spawn a new bullet after the delay
                    StartCoroutine(SpawnAfterDelay());

                    break;
                }
            }
        }
    }
}
