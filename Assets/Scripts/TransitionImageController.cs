/*
 * TransitionImageController.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script controls the fade-in and fade-out process of an image, by changing the alpha (opacity) of its colour over time.
 * 
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TransitionImageController : MonoBehaviour
{
    public float transitionTime = 1f;
    private Image transitionImage;
    private Canvas canvas; // Reference to the Canvas component

    private void Awake()
    {
        transitionImage = GetComponent<Image>();
        canvas = GetComponentInParent<Canvas>(); // Get the Canvas component from the parent GameObject
    }

    // Method to start the fade-in process (image becomes visible)
    public void FadeIn()
    {
        canvas.sortingOrder = 2; // Set the sorting order to 2 before fading in
        StartCoroutine(Fade(0, 1));
    }

    // Method to start the fade-out process (image becomes invisible)
    public void FadeOut()
    {
        StartCoroutine(Fade(1, 0, () => canvas.sortingOrder = -1)); // Set the sorting order to -1 after fading out
    }

    // Coroutine to handle the fade process (either fade-in or fade-out)
    // startAlpha: The initial alpha value (opacity) of the image
    // endAlpha: The target alpha value (opacity) of the image
    // onComplete: Optional action to perform after the fade process is complete
    private IEnumerator Fade(float startAlpha, float endAlpha, System.Action onComplete = null)
    {
        float progress = 0;

        while (progress < transitionTime)
        {
            progress += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, progress / transitionTime);
            transitionImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        onComplete?.Invoke(); // Execute the onComplete action, if provided
    }
}
