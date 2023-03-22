using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSelection : MonoBehaviour
{
    // Array of weapon sprites
    [SerializeField] private Sprite[] weaponSprites;

    // GameObject that contains the image of the currently selected bullet
    [SerializeField] private GameObject bulletBoxObject;

    // Index of the currently selected weapon
    public int currentWeaponIndex = 0;

    // Number of bullets for each type of bullet
    public int bullet1Count = 0;
    public int bullet2Count = 0;
    public int bullet3Count = 0;

    // Text elements displaying the number of bullets for each type of bullet
    [SerializeField] private TextMeshProUGUI bullet1CountText;
    [SerializeField] private TextMeshProUGUI bullet2CountText;
    [SerializeField] private TextMeshProUGUI bullet3CountText;

    // Image component of the bullet box object
    private Image bulletBoxImage;

    // Transform representing the position to spawn bullets
    [SerializeField] private Transform firePoint;

    // Prefabs for each type of bullet
    [SerializeField] private GameObject bullet1Prefab;
    [SerializeField] private GameObject bullet2Prefab;
    [SerializeField] private GameObject bullet3Prefab;

    // Sound effect played when a bullet is collected
    [SerializeField] private AudioSource collectSound;


    private void Start()
    {
        // Set the initial weapon index to 0 (corresponding to the specific bullet)
        currentWeaponIndex = 0;

        // Get the Image component of the BulletBoxIMG object
        bulletBoxImage = bulletBoxObject.GetComponent<Image>();
    }

    private void Update()
    {
        // Update the bullet count text for each type of bullet
        UpdateBullet1CountText();
        UpdateBullet2CountText();
        UpdateBullet3CountText();

        if (Input.GetKeyDown(KeyCode.V))
        {
            // Increment the current weapon index and wrap it around to 0 if it exceeds the array length
            currentWeaponIndex = (currentWeaponIndex + 1) % weaponSprites.Length;

            // Update the weapon image
            bulletBoxImage.sprite = weaponSprites[currentWeaponIndex];
        }

        if (Input.GetButtonDown("Fire1"))
        {
            // Check if the current weapon index is 0 (corresponding to the specific bullet)
            if (currentWeaponIndex == 0)
            {
                if (bullet1Count > 0)
                {
                    // Decrease the value of the bullet1Count variable and spawn a bullet1Prefab at the fire point
                    bullet1Count--;
                    Instantiate(bullet1Prefab, firePoint.position, firePoint.rotation);
                }
            }
            // Check if the current weapon index is 1 (corresponding to the second bullet)
            else if (currentWeaponIndex == 1)
            {
                if (bullet2Count > 0)
                {
                    // Decrease the value of the bullet2Count variable and spawn a bullet2Prefab at the fire point
                    bullet2Count--;
                    Instantiate(bullet2Prefab, firePoint.position, firePoint.rotation);
                }
            }
            // Check if the current weapon index is 2 (corresponding to the third bullet)
            else if (currentWeaponIndex == 2)
            {
                if (bullet3Count > 0)
                {
                    // Decrease the value of the bullet3Count variable and spawn a bullet3Prefab at the fire point
                    bullet3Count--;
                    Instantiate(bullet3Prefab, firePoint.position, firePoint.rotation);
                }
            }
        }
    }
    // Method called when the object collides with a trigger collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the "Bullet1Collect" tag
        if (collision.gameObject.CompareTag("Bullet1Collect"))
        {
            // Check if the bullet1Count is less than the maximum value of 9
            if (bullet1Count < 9)
            {
                // Destroy the collided object, increase the bullet1Count and play the collect sound effect
                Destroy(collision.gameObject);
                bullet1Count++;
                collectSound.Play();
            }
        }
        // Check if the collided object has the "Bullet2Collect" tag
        else if (collision.gameObject.CompareTag("Bullet2Collect"))
        {
            // Check if the bullet2Count is less than the maximum value of 9
            if (bullet2Count < 9)
            {
                // Destroy the collided object, increase the bullet2Count and play the collect sound effect
                Destroy(collision.gameObject);
                bullet2Count++;
                collectSound.Play();
            }
        }
        // Check if the collided object has the "Bullet3Collect" tag
        else if (collision.gameObject.CompareTag("Bullet3Collect"))
        {
            // Check if the bullet3Count is less than the maximum value of 9
            if (bullet3Count < 9)
            {
                // Destroy the collided object, increase the bullet3Count and play the collect sound effect
                Destroy(collision.gameObject);
                bullet3Count++;
                collectSound.Play();
            }
        }
    }

    // Method to update the bullet1CountText element
    public void UpdateBullet1CountText()
    {
        bullet1CountText.text = bullet1Count.ToString();
        // Change the color of the text to red if the bullet count is at the maximum value of 9
        if (bullet1Count == 9)
        {
            bullet1CountText.color = Color.red;
        }
        else
        {
            bullet1CountText.color = Color.black;
        }
    }

    // Method to update the bullet2CountText element
    public void UpdateBullet2CountText()
    {
        bullet2CountText.text = bullet2Count.ToString();
        // Change the color of the text to red if the bullet count is at the maximum value of 9
        if (bullet2Count == 9)
        {
            bullet2CountText.color = Color.red;
        }
        else
        {
            bullet2CountText.color = Color.black;
        }
    }

    // Method to update the bullet3CountText element
    public void UpdateBullet3CountText()
    {
        bullet3CountText.text = bullet3Count.ToString();
        // Change the color of the text to red if the bullet count is at the maximum value of 9
        if (bullet3Count == 9)
        {
            bullet3CountText.color = Color.red;
        }
        else
        {
            bullet3CountText.color = Color.black;
        }
    }
}