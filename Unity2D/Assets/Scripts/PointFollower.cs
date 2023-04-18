using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] points;
    private int currentPointIndex = 0;

    [SerializeField] private float speed = 2f;

    [SerializeField] private bool alwaysOn = false;

    // Reference to the player's transform
    [SerializeField] private Transform player;

    // Distance threshold
    [SerializeField] private float maxDistance = 15f;

    private void Update()
    {
        bool shouldMove = false;

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

        if (shouldMove)
        {
            if (Vector2.Distance(points[currentPointIndex].transform.position, transform.position) < .1f)
            {
                currentPointIndex++;
                if (currentPointIndex >= points.Length)
                {
                    currentPointIndex = 0;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, points[currentPointIndex].transform.position, Time.deltaTime * speed);
        }
    }
}