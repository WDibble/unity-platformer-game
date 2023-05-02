/*
 * CameraVignette.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * WIP - UNUSED
 * This script is used to control the intensity of the vignette effect on the camera based on whether the player is dissolving (becoming invisible) or not. 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraVignette : MonoBehaviour
{
    [SerializeField] private Dissolve dissolve; // Reference to the Dissolve script
    private PostProcessVolume volume; // Reference to the camera's PostProcessVolume component

    PostProcessProfile profile;
    Vignette vignette;

    void Start()
    {
        volume = GetComponent<PostProcessVolume>();
        profile = volume.profile;
        profile.TryGetSettings(out vignette);
    }

    void Update()
    {
        if (dissolve.PlayerIsDissolving())
        {
            vignette.intensity.value += Time.deltaTime;

            if (vignette.intensity.value >= 1f)
            {
                vignette.intensity.value = 1f;
            }
        }

        else if (!dissolve.PlayerIsDissolving())
        {
            vignette.intensity.value -= Time.deltaTime;

            if (vignette.intensity.value <= 0f)
            {
                vignette.intensity.value = 0f;
            }
        }
    }
}