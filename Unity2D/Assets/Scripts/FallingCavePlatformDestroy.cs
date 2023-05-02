/*
 * FallingCavePlatformDestroy.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script is used to destroy a falling cave platform after its animation has played out.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCavePlatformDestroy : MonoBehaviour
{
    [SerializeField] private float delay = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay);
    }
}
