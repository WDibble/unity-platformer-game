/*
 * ItemCollector.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script handles the collection and display of different types of bullets in the game.
 * It detects collisions with bullet collectibles and updates the count and display of each type of bullet.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    // Variables to store the number of bullets collected
    private int bullet1Collectables = 0;
    private int bullet2Collectables = 0;
    private int bullet3Collectables = 0;

    // Text components to display bullet counts on the screen
    [SerializeField] private TextMeshProUGUI bullet1CollectableText;
    [SerializeField] private TextMeshProUGUI bullet2CollectableText;
    [SerializeField] private TextMeshProUGUI bullet3CollectableText;

    // Audio source to play the collect sound
    [SerializeField] private AudioSource collectSound;

    // Detect collisions with bullet collectibles
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Collect bullet type 1
        if (collision.gameObject.CompareTag("Bullet1Collect"))
        {
            if (bullet1Collectables < 9)
            {
                Destroy(collision.gameObject);
                bullet1Collectables = Mathf.Clamp(bullet1Collectables + 1, 0, 9);
                UpdateBullet1CountText();
                collectSound.Play();
            }

            // Change the color of the text if the maximum count is reached
            if (bullet1Collectables == 9)
            {
                bullet1CollectableText.color = Color.red;
            }
            else
            {
                bullet1CollectableText.color = Color.black;
            }
        }
        // Collect bullet type 2
        else if (collision.gameObject.CompareTag("Bullet2Collect"))
        {
            if (bullet2Collectables < 9)
            {
                Destroy(collision.gameObject);
                bullet2Collectables = Mathf.Clamp(bullet2Collectables + 1, 0, 9);
                UpdateBullet2CountText();
                collectSound.Play();
            }

            // Change the color of the text if the maximum count is reached
            if (bullet2Collectables == 9)
            {
                bullet2CollectableText.color = Color.red;
            }
            else
            {
                bullet2CollectableText.color = Color.black;
            }
        }
        // Collect bullet type 3
        else if (collision.gameObject.CompareTag("Bullet3Collect"))
        {
            if (bullet3Collectables < 9)
            {
                Destroy(collision.gameObject);
                bullet3Collectables = Mathf.Clamp(bullet3Collectables + 1, 0, 9);
                UpdateBullet3CountText();
                collectSound.Play();
            }

            // Change the color of the text if the maximum count is reached
            if (bullet3Collectables == 9)
            {
                bullet3CollectableText.color = Color.red;
            }
            else
            {
                bullet3CollectableText.color = Color.black;
            }
        }
    }

    // Update bullet count text for each type of bullet
    public void UpdateBullet1CountText()
    {
        bullet1CollectableText.text = bullet1Collectables.ToString();
    }

    public void UpdateBullet2CountText()
    {
        bullet2CollectableText.text = bullet2Collectables.ToString();
    }

    public void UpdateBullet3CountText()
    {
        bullet3CollectableText.text = bullet3Collectables.ToString();
    }
}
