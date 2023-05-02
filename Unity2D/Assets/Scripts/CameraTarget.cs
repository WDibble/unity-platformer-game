/*
 * CameraTarget.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script is used to lock the rotation of the camera to its initial rotation.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    private Quaternion initialRotation;

    void Start()
    {
        // Save the initial rotation of the object
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // Reset the rotation of the object every frame
        transform.rotation = initialRotation;
    }
}