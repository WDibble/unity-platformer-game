using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet1Prefab;
    [SerializeField] private GameObject bullet2Prefab;
    [SerializeField] private GameObject bullet3Prefab;

    private WeaponSelection weaponSelection;

    private void Start()
    {
        // Find the WeaponSelection script attached to the same game object
        weaponSelection = GetComponent<WeaponSelection>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // Check which weapon is currently selected
            switch (weaponSelection.currentWeaponIndex)
            {
                case 0:
                    // Check if the player has any bullet1 left
                    if (weaponSelection.bullet1Count > 0)
                    {
                        // Instantiate the bullet1 prefab and decrease the bullet1 count
                        Instantiate(bullet1Prefab, firePoint.position, firePoint.rotation);
                    }
                    break;
                case 1:
                    // Check if the player has any bullet2 left
                    if (weaponSelection.bullet2Count > 0)
                    {
                        // Instantiate the bullet2 prefab and decrease the bullet2 count
                        Instantiate(bullet2Prefab, firePoint.position, firePoint.rotation);
                    }
                    break;
                case 2:
                    // Check if the player has any bullet3 left
                    if (weaponSelection.bullet3Count > 0)
                    {
                        // Instantiate the bullet3 prefab and decrease the bullet3 count
                        Instantiate(bullet3Prefab, firePoint.position, firePoint.rotation);
                    }
                    break;
            }
        }
    }
}