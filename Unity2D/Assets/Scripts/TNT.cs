/*
 * TransitionImageController.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script controls the behavior of a TNT object, including detecting collisions
 * spawning an explosion when it collides with an object that is not tagged as "CameraConfiner" or "Trap".
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    // Serialize the TNT_Impact field to allow for customization in Unity Editor
    [SerializeField] private GameObject TNT_Impact;

    // OnTriggerEnter2D is called when the TNT collider enters another collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object's tag is not "CameraConfiner"
        if (!collision.gameObject.CompareTag("CameraConfiner") || !collision.gameObject.CompareTag("Trap"))
        {
            // Instantiate the TNT_Impact prefab at the TNT's position and rotation
            Instantiate(TNT_Impact, transform.position, transform.rotation);

            // Destroy the TNT game object
            Destroy(gameObject);
        }
    }
}
