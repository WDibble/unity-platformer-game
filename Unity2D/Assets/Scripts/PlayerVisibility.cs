/*
 * PlayerVisibility.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * Stores and retrieves the visibility status of the player.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisibility : MonoBehaviour
{
    public static bool IsInvisible { get; private set; }

    // Set invisiblity status of the player
    public static void SetInvisible(bool invisible)
    {
        IsInvisible = invisible;
    }

    // Get invisiblity status of the player
    public static bool GetInvisible()
    {
        return IsInvisible;
    }
}