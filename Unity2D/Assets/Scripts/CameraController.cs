/*
 * CameraController.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script simply sets the position of the camera to the position of the player's transform every frame in the Update() method.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
