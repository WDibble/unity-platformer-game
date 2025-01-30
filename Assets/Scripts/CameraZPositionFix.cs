/*
 * CameraZPositionFix.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script is used to fix the z-position of the camera in the game.
 * 
 */

using UnityEngine;

public class CameraZPositionFix : MonoBehaviour
{
    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.z = Mathf.Abs(pos.z) * -1f;
        transform.position = pos;
    }
}