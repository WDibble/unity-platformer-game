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
