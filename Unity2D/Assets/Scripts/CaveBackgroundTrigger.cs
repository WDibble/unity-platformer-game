/*
 * CaveBackgroundTrigger.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script is attached to a trigger collider in the game world, and is used to activate 
 * the cave background when the player enters the trigger zone.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveBackgroundTrigger : MonoBehaviour
{
    [SerializeField] private GameObject caveBackground;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!caveBackground.activeInHierarchy)
            {
                caveBackground.SetActive(true);
            }
        }
    }
}
