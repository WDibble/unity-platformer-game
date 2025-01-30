/*
 * TNT_Impact.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script handles the destruction of TNT and plays the explosion sound when it hits the ground.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT_Impact : MonoBehaviour
{
    // Serialize the delay field to allow for customization in Unity Editor
    [SerializeField] private float delay = 0.3f;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        // Find the AudioManager object in the scene
        audioManager = FindObjectOfType<AudioManager>();

        // Play the explosion sound when the TNT hits the ground
        audioManager.PlayExplosionSound();

        // Destroy the TNT game object after the specified delay
        Destroy(gameObject, delay);
    }
}
