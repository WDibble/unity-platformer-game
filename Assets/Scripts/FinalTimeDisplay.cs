/*
 * FinalTimeDisplay.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script is in charge of displaying the final time of the level once the player completes it.
 * It uses a Timer object to get the final time and animates the display of the time by counting up to the final time over a specified duration.
 */

using System.Collections;
using UnityEngine;
using TMPro;

public class FinalTimeDisplay : MonoBehaviour
{
    private Timer timer;
    public float countUpDuration = 2f; // Time for the counting up animation

    // Start is called before the first frame update
    private void Start()
    {
        // Find the Timer object in the scene
        timer = FindObjectOfType<Timer>();

        // If a Timer object is found, start the AnimateFinalTime coroutine
        if (timer != null)
        {
            StartCoroutine(AnimateFinalTime());
        }
        else
        {
            // If no Timer object is found, display a "Not available" message
            GetComponent<TextMeshProUGUI>().text = "Final Time: Not available";
        }
    }

    // Coroutine to animate the display of the final time
    private IEnumerator AnimateFinalTime()
    {
        // Get the final time from the Timer object
        float finalTime = timer.GetTime();
        float elapsedTime = 0f;

        // Count up to the final time over the duration specified
        while (elapsedTime < countUpDuration)
        {
            // Update the elapsed time
            elapsedTime += Time.deltaTime;
            // Calculate the progress as a fraction of the countUpDuration
            float progress = elapsedTime / countUpDuration;
            // Calculate the displayed time by interpolating between 0 and finalTime based on progress
            float displayedTime = Mathf.Lerp(0, finalTime, progress);

            // Update the displayed time in the TextMeshProUGUI component
            GetComponent<TextMeshProUGUI>().text = "Final Time:\n" + displayedTime.ToString("F0") + " Seconds";
            yield return null;
        }

        // Set the final displayed time to the actual final time
        GetComponent<TextMeshProUGUI>().text = "Final Time:\n" + finalTime.ToString("F0") + " Seconds";
    }
}
