/*
 * TransitionImageController.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script is responsible for controlling the Cinemachine Virtual Camera component, specifically its orthographic size.
 * 
 * The orthographic size determines how much of the game world is visible to the player, 
 * with a smaller value showing less of the world and a larger value showing more.
 */

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinemachineController : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private float originalOrthoSize;
    [SerializeField] private float invisibleOrthoSize;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Cinemachine Virtual Camera component and store the original orthographic size
        vcam = GetComponent<CinemachineVirtualCamera>();
        originalOrthoSize = vcam.m_Lens.OrthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current scene name
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Return early if the current scene is Level 3 or Level 4, as no changes are needed
        if (currentSceneName == "Level 3" || currentSceneName == "Level 4")
        {
            return;
        }

        // Get the player's visibility state
        bool isInvisible = PlayerVisibility.IsInvisible;

        // If the player is invisible and the camera's orthographic size is not set to invisibleOrthoSize, interpolate towards it
        if (isInvisible && vcam.m_Lens.OrthographicSize != invisibleOrthoSize)
        {
            vcam.m_Lens.OrthographicSize = Mathf.Lerp(vcam.m_Lens.OrthographicSize, invisibleOrthoSize, Time.deltaTime * 2);
        }
        // If the player is not invisible and the camera's orthographic size is not set to the originalOrthoSize, interpolate towards it
        else if (!isInvisible && vcam.m_Lens.OrthographicSize != originalOrthoSize)
        {
            vcam.m_Lens.OrthographicSize = Mathf.Lerp(vcam.m_Lens.OrthographicSize, originalOrthoSize, Time.deltaTime * 2);
        }
    }
}