using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicStartScreen : MonoBehaviour
{
    // Multiplier for the parallax effect strength
    public float parallaxEffectMultiplier = 0.1f;

    // Controls the smoothness of the movement
    public float smoothness = 5f;

    // The distance at which the parallax effect reduces to zero
    public float distanceEffectRange = 50f;

    // Factor for controlling the parallax effect for each layer (set in the Inspector)
    public float parallaxLayerFactor = 1f;

    // The starting position of the background layer
    private Vector3 startPosition;

    // Width and height of the screen
    private float screenWidth;
    private float screenHeight;

    void Start()
    {
        // Store the initial position of the background layer
        startPosition = transform.position;

        // Get the screen dimensions
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    void Update()
    {
        // Get the mouse coordinates on the screen
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        // Calculate the distance from the initial position
        float distance = Vector3.Distance(transform.position, startPosition);

        // Calculate the adjusted parallax effect multiplier based on the distance and layer factor
        float adjustedMultiplier = Mathf.Lerp(parallaxEffectMultiplier, 0, distance / distanceEffectRange) * parallaxLayerFactor;

        // Calculate the horizontal and vertical movement based on the mouse position and adjusted multiplier
        float deltaX = (mouseX - screenWidth * 0.5f) * adjustedMultiplier;
        float deltaY = (mouseY - screenHeight * 0.5f) * adjustedMultiplier;

        // Determine the target position for the layer based on the calculated movement
        Vector3 targetPosition = new Vector3(startPosition.x + deltaX, startPosition.y + deltaY, startPosition.z);

        // Smoothly move the layer towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.unscaledDeltaTime);
    }
}
