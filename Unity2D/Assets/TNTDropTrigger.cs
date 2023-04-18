using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTDropTrigger : MonoBehaviour
{
    [SerializeField] private Transform dropPoint;
    [SerializeField] private GameObject TNTPrefab;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player") && !PlayerVisibility.GetInvisible())
        {
            triggered = true;
            StartCoroutine(SpawnTNTs());
        }
    }

    IEnumerator SpawnTNTs()
    {
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 8; i++)
            {
                Instantiate(TNTPrefab, dropPoint.position + new Vector3(i, 0, 0), dropPoint.rotation);
                yield return new WaitForSeconds(0.5f);
            }
            if (j < 2)
            {
                yield return new WaitForSeconds(3f); // Wait 3 seconds before the next repetition
            }
        }
    }
}