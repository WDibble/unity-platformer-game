/*
 * BatPointFollower.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script is used for the bat enemies that move between a set of points in Level 3.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatPointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] points; // An array of GameObjects representing the points to follow
    private int currentPointIndex = 0; // Index of the current point the bat is moving towards

    [SerializeField] private float speed = 2f; // Speed at which the bat moves between points

    private Animator animator; // Reference to the Animator component

    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to the Animator component
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.enabled = true; // Enable the animator
        Vector2 previousPosition = transform.position; // Store the bat's previous position

        // Check if the bat is close enough to the current point
        if (Vector2.Distance(points[currentPointIndex].transform.position, transform.position) < .1f)
        {
            // Move to the next point in the array
            currentPointIndex++;
            // If the current point index is beyond the array length, reset it to 0
            if (currentPointIndex >= points.Length)
            {
                currentPointIndex = 0;
            }
        }
        // Move the bat towards the current point
        transform.position = Vector2.MoveTowards(transform.position, points[currentPointIndex].transform.position, Time.deltaTime * speed);

        // Update the bat's direction based on its moving direction
        UpdateObjectDirection(previousPosition);
    }

    // Update the object's direction based on the moving direction
    private void UpdateObjectDirection(Vector2 previousPosition)
    {
        float direction = transform.position.x - previousPosition.x; // Calculate the moving direction

        // Check if the bat is moving right
        if (direction > 0.01f)
        {
            // Rotate the bat to face right
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        // Check if the bat is moving left
        else if (direction < -0.01f)
        {
            // Rotate the bat to face left
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
