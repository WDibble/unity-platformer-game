/*
 * BreakablePlatform.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script is attached to a breakable platform game object, and it allows the platform to break when the player collides with it.
 * 
 */

using System.Collections;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    [SerializeField] private GameObject breakingPlatformPrefab; // Prefab for the breaking platform animation
    [SerializeField] private float delayBeforeBreaking = 0.5f; // Time delay before the platform breaks
    private AudioManager audioManager; // Reference to the AudioManager

    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to the AudioManager object
        audioManager = FindObjectOfType<AudioManager>();
    }

    // OnCollisionEnter2D is called when the platform collides with another object
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Check if the colliding object has the "Player" tag
        if (other.gameObject.CompareTag("Player"))
        {
            // Start the BreakPlatform coroutine
            StartCoroutine(BreakPlatform());
        }
    }

    // Coroutine to break the platform after a specified delay
    private IEnumerator BreakPlatform()
    {
        // Wait for the specified delay before breaking
        yield return new WaitForSeconds(delayBeforeBreaking);

        // Instantiate the breaking platform animation object
        Instantiate(breakingPlatformPrefab, transform.position, Quaternion.identity);
        // Play the fall impact sound effect
        audioManager.PlayFallImpactSound();

        // Destroy the breakable platform object
        Destroy(gameObject);
    }
}

