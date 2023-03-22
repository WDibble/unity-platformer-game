using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int bullet1Collectables = 0;
    private int bullet2Collectables = 0;
    private int bullet3Collectables = 0;

    [SerializeField] private TextMeshProUGUI bullet1CollectableText;
    [SerializeField] private TextMeshProUGUI bullet2CollectableText;
    [SerializeField] private TextMeshProUGUI bullet3CollectableText;

    [SerializeField] private AudioSource collectSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet1Collect"))
        {
            if (bullet1Collectables < 9)
            {
                Destroy(collision.gameObject);
                bullet1Collectables = Mathf.Clamp(bullet1Collectables + 1, 0, 9);
                UpdateBullet1CountText();
                collectSound.Play();
            }

            if (bullet1Collectables == 9)
            {
                bullet1CollectableText.color = Color.red;
            }
            else
            {
                bullet1CollectableText.color = Color.black;
            }
        }
        else if (collision.gameObject.CompareTag("Bullet2Collect"))
        {
            if (bullet2Collectables < 9)
            {
                Destroy(collision.gameObject);
                bullet2Collectables = Mathf.Clamp(bullet2Collectables + 1, 0, 9);
                UpdateBullet2CountText();
                collectSound.Play();
            }

            if (bullet2Collectables == 9)
            {
                bullet2CollectableText.color = Color.red;
            }
            else
            {
                bullet2CollectableText.color = Color.black;
            }
        }
        else if (collision.gameObject.CompareTag("Bullet3Collect"))
        {
            if (bullet3Collectables < 9)
            {
                Destroy(collision.gameObject);
                bullet3Collectables = Mathf.Clamp(bullet3Collectables + 1, 0, 9);
                UpdateBullet3CountText();
                collectSound.Play();
            }

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
