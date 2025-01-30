/*
 * PlatformStick.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script makes the player stick to the platform they are touching by making the player a child of the platform,
 * and de-parents the player when they leave the platform.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStick : MonoBehaviour
{
    // If the player is touching the platform, make the player a child of the platform so that they stick to it
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    // When the player leaves the platform, de-parent it from the platform
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.transform.SetParent(null);
    }
}
