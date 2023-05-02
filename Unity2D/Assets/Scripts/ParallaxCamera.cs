/*
 * ParallaxCamera.cs
 * Author: William Dibble
 * Date: 24-04-2023
 * 
 * This script is attached to the main camera of the game. 
 * It detects the camera's movement and triggers the onCameraTranslate event, 
 * passing the change in position (delta) as a parameter. The event is used to inform other 
 * scripts (like ParallaxLayer) about the camera's movement, so they can adjust their behavior accordingly.
 *
 */

using UnityEngine;

[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour
{
    // Declare a delegate to handle camera movement events
    public delegate void ParallaxCameraDelegate(Vector2 deltaMovement);

    // Create an event to handle camera movement
    public ParallaxCameraDelegate onCameraTranslate;

    // Store the old position of the camera
    private Vector2 oldPosition;

    // Initialize the old position with the camera's position
    void Start()
    {
        oldPosition = transform.position;
    }

    // Check if the camera's position has changed and trigger the onCameraTranslate event
    void FixedUpdate()
    {
        if ((Vector2)transform.position != oldPosition)
        {
            if (onCameraTranslate != null)
            {
                Vector2 delta = oldPosition - (Vector2)transform.position;
                onCameraTranslate(delta);
            }
            oldPosition = transform.position;
        }
    }
}
