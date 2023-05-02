/*
 * PointFollower.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * The PointFollower script is used to make an object move between a series of points.
 * This is used for the blades, the moving platforms, and the invisible bats.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] points; // Array of points to move between
    
    private int currentPointIndex = 0; // Current point index

    [SerializeField] private float speed = 2f; // Movement speed

    [SerializeField] private bool alwaysOn = false; // If true, the object always moves regardless of player's distance

    // Reference to the player's transform
    [SerializeField] private Transform player;

    // Distance threshold for starting the movement if not always on
    [SerializeField] private float maxDistance = 15f;

    private void Update()
    {
        bool shouldMove = false;

        // Check conditions for the object to move
        if (gameObject.CompareTag("Platform"))
        {
            shouldMove = true;
        }
        else if (alwaysOn)
        {
            shouldMove = true;
        }
        else if (Vector2.Distance(player.position, transform.position) <= maxDistance)
        {
            shouldMove = true;
        }

        // If the object should move, update its position
        if (shouldMove)
        {
            // If the object has reached its current point, move to the next point in the array
            if (Vector2.Distance(points[currentPointIndex].transform.position, transform.position) < .1f)
            {
                currentPointIndex++;
                if (currentPointIndex >= points.Length)
                {
                    currentPointIndex = 0;
                }
            }

            // Move the object towards the current point
            transform.position = Vector2.MoveTowards(transform.position, points[currentPointIndex].transform.position, Time.deltaTime * speed);
        }
    }
}