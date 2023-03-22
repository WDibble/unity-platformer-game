using System.Collections;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    [SerializeField] private GameObject breakingPlatformPrefab;
    [SerializeField] private float delayBeforeBreaking = 0.5f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(BreakPlatform());
        }
    }

    private IEnumerator BreakPlatform()
    {
        yield return new WaitForSeconds(delayBeforeBreaking);

        // Instantiate the breaking platform animation object
        Instantiate(breakingPlatformPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}

