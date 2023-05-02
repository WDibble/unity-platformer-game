/*
 * BackgroundChange.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script is attached to an object in the game and changes the background when the player enters a specific trigger area.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChange : MonoBehaviour
{
    [SerializeField] private GameObject background1_1;
    [SerializeField] private GameObject background1_2;
    [SerializeField] private GameObject background1_3;
    [SerializeField] private GameObject background1_4;
    [SerializeField] private GameObject background2_1;

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BackgroundTrigger"))
        {
            if (!background2_1.activeInHierarchy)
            {
                background2_1.SetActive(true);
            }
        }
    }

}
