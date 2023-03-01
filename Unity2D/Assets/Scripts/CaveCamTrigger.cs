using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CaveCamTrigger : MonoBehaviour
{
    public float newYFollowOffset1 = -3f;
    public float newYFollowOffset2 = 3f;
    [SerializeField] private CinemachineVirtualCamera cam;

    private void OnTriggerEnter2D(Collider2D other)
    {
        CinemachineComposer composer = cam.GetCinemachineComponent<CinemachineComposer>();
        if (other.CompareTag("CaveCamTrigger1"))
        {
            composer.m_TrackedObjectOffset.y = newYFollowOffset1;
        }
        else if (other.CompareTag("CaveCamTrigger2"))
        {
            composer.m_TrackedObjectOffset.y = newYFollowOffset2;
        }
    }
}