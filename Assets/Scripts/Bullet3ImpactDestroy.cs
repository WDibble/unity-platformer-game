/*
 * Bullet3ImpactDestroy.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script is used to destroy the bullet impact prefab effect with a delay after it is instantiated.
 * 
 */

using UnityEngine;

public class Bullet3ImpactDestroy : MonoBehaviour
{
    [SerializeField] private float delay = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay);
    }
}