/*
 * TNTDropTrigger.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script spawns one or multiple TNT prefabs in a row when a trigger is entered, with a delay between each spawn.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTDropTrigger : MonoBehaviour
{
    // Serialize fields to allow for customization in Unity Editor
    [SerializeField] private Transform dropPoint;
    [SerializeField] private GameObject TNTPrefab;

    private bool triggered = false;

    // OnTriggerEnter2D is called when the collider enters the trigger zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the trigger hasn't been activated yet, if the other object is the player, and if the player isn't invisible
        if (!triggered && other.CompareTag("Player") && !PlayerVisibility.GetInvisible())
        {
            triggered = true;
            StartCoroutine(SpawnTNTs());
        }
    }

    // Coroutine for spawning TNTs
    IEnumerator SpawnTNTs()
    {
        // Repeat the TNT spawning process 3 times
        for (int j = 0; j < 3; j++)
        {
            // Spawn 8 TNTs in a row
            for (int i = 0; i < 8; i++)
            {
                // Instantiate a TNT prefab at the drop point position with an offset (i, 0, 0) and the drop point rotation
                Instantiate(TNTPrefab, dropPoint.position + new Vector3(i, 0, 0), dropPoint.rotation);
                // Wait for 0.5 seconds before spawning the next TNT
                yield return new WaitForSeconds(0.5f);
            }
            // After spawning the 8 TNTs, wait for 3 seconds before repeating the process, if there are more repetitions left
            if (j < 2)
            {
                yield return new WaitForSeconds(3f);
            }
        }
    }
}