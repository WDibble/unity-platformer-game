/*
 * TimerUICounter.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script updates the TextMeshProUGUI component of the TimerUI with the current time value from the Timer script, formatted as a whole number.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerUICounter : MonoBehaviour
{
    // Declare a reference to the Timer script
    private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        // Find the Timer script in the scene and assign it to the timer variable
        timer = FindObjectOfType<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the TextMeshProUGUI component attached to the game object
        // Set its text to the current time value from the Timer script, formatted as a whole number
        GetComponent<TextMeshProUGUI>().text = timer.GetTime().ToString("F0");
    }
}
