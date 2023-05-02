/*
 * ParallaxLayer.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script is attached to individual layers of the parallax background. 
 * It listens to the onCameraTranslate event from the ParallaxCamera script and moves the layer 
 * accordingly based on the camera's movement and the parallax factor. The parallax factor is used 
 * to create a depth effect, making some layers move slower or faster, giving the illusion of a 3D environment.
 *
 */

using UnityEngine;

[ExecuteInEditMode]
public class ParallaxLayer : MonoBehaviour
{
    // Parallax factor to control the speed of the layer movement
    public float parallaxFactor;

    // Move the layer based on the camera's movement and parallax factor
    public void Move(Vector2 delta)
    {
        // Calculate the new position of the layer based on the camera movement and parallax factor
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta.x * parallaxFactor;
        newPos.y -= delta.y * parallaxFactor;

        // Update the layer's position
        transform.localPosition = newPos;
    }
}
