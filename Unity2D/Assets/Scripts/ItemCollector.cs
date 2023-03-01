using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int collectables = 0;

    [SerializeField] private TextMeshProUGUI collectableText;

    [SerializeField] private AudioSource collectSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            Destroy(collision.gameObject);
            collectables++;
            collectableText.text = collectables.ToString();
            collectSound.Play();
        }
    }
}
