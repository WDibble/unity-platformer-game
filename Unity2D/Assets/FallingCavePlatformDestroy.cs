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
