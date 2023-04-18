using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;

    // Reference to the player's transform
    [SerializeField] private Transform player;

    // Distance threshold
    [SerializeField] private float maxDistance = 15f;

    private void Update()
    {
        if (Vector2.Distance(player.position, transform.position) <= maxDistance)
        {
            transform.Rotate(0, 0, 360 * speed * Time.deltaTime);
        }
    }
}