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