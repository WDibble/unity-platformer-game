/*
 * Dissolve.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script is a component attached to a game object that provides the ability for the 
 * player to become invisible and visible again through a dissolve effect.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    Material material;

    bool isDissolving = false;
    float fade = 1f;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the material variable with the material of the SpriteRenderer component
        material = GetComponent<SpriteRenderer>().material;
        // Get the AudioManager component to play sounds later
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the "F" key has been pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            // If the player is not invisible, make them invisible and start the dissolve process
            if (!PlayerVisibility.IsInvisible)
            {
                PlayerVisibility.SetInvisible(true);
                isDissolving = true;
                // Play the sound effect for becoming invisible
                audioManager.PlayInvisible1();
            }
            // If the player is already invisible, make them visible and reverse the dissolve process
            else
            {
                PlayerVisibility.SetInvisible(false);
                isDissolving = false;
                // Play the sound effect for becoming visible
                audioManager.PlayInvisible2();
            }
        }

        // If the player is dissolving (becoming invisible)
        if (isDissolving)
        {
            // Decrease the fade value over time to make the player more transparent
            fade -= Time.deltaTime;

            // Make sure the fade value doesn't go below 0
            if (fade <= 0f)
            {
                fade = 0f;
            }

            // Set the "_Fade" property of the material to the new fade value
            material.SetFloat("_Fade", fade);
        }

        // If the player is not dissolving (becoming visible)
        else if (!isDissolving)
        {
            // Increase the fade value over time to make the player less transparent
            fade += Time.deltaTime;

            // Make sure the fade value doesn't go above 1
            if (fade >= 1f)
            {
                fade = 1f;
            }

            // Set the "_Fade" property of the material to the new fade value
            material.SetFloat("_Fade", fade);
        }
    }

    // Public method to access the isDissolving field
    public bool PlayerIsDissolving()
    {
        // Return the current state of the isDissolving field
        return isDissolving;
    }

}