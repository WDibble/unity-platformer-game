/*
 * ParallaxBackground.cs
 * Author: William Dibble
 * Date: 24-04-2023
 * 
 * This script is attached to a GameObject representing the parallax background. 
 * It initializes and manages the parallax layers, which are its child GameObjects with ParallaxLayer components. 
 * The script listens to the onCameraTranslate event from the ParallaxCamera script, 
 * updating the positions of the parallax layers according to the camera's movement. 
 * This script is responsible for creating the overall parallax effect by coordinating the movements of all the individual layers.
 *
 */

using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParallaxBackground : MonoBehaviour
{
    public ParallaxCamera parallaxCamera;
    List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

    void Start()
    {
        // Set parallaxCamera to the main camera's ParallaxCamera component if not assigned
        if (parallaxCamera == null)
            parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();

        // Subscribe to onCameraTranslate event
        if (parallaxCamera != null)
            parallaxCamera.onCameraTranslate += UpdateLayerPositions;

        // Initialize parallax layers
        InitializeLayers();
    }

    // Initialize parallaxLayers list with child objects containing ParallaxLayer components
    void InitializeLayers()
    {
        parallaxLayers.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();

            if (layer != null)
            {
                layer.name = "Layer_" + i;
                parallaxLayers.Add(layer);
            }
        }
    }

    // Update the position of each parallax layer based on the camera's movement
    void UpdateLayerPositions(Vector2 delta)
    {
        foreach (ParallaxLayer layer in parallaxLayers)
        {
            layer.Move(delta);
        }
    }
}
