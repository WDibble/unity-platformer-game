/*
 * BackgroundChangeL2.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script is responsible for triggering the fade out effect on a specific background 
 * object when the player enters a trigger with the "BackgroundTrigger" tag.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChangeL2 : MonoBehaviour
{
    [SerializeField] private GameObject background2_1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BackgroundTrigger"))
        {
            FadeOut fadeOutScript = background2_1.GetComponent<FadeOut>();
            if (fadeOutScript != null)
            {
                fadeOutScript.StartFadeOut();
            }
        }
    }

}