using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuDissolve : MonoBehaviour
{
    public float fadeSpeed = 1f;
    private CanvasGroup canvasGroup;
    private bool isVisible;

    // Start is called before the first frame update
    void Start()
    {
        // Get the CanvasGroup component attached to the PauseMenu UI object
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the PauseMenu UI object is active and not yet fully visible
        if (gameObject.activeInHierarchy && !isVisible)
        {
            FadeIn();
        }
        // Check if the PauseMenu UI object is inactive and not yet fully invisible
        else if (!gameObject.activeInHierarchy && isVisible)
        {
            FadeOut();
        }
    }

    // Fade in all child elements of the PauseMenu UI object
    void FadeIn()
    {
        // Increase the alpha value of the CanvasGroup
        canvasGroup.alpha += fadeSpeed * Time.unscaledDeltaTime;

        // Clamp the alpha value at 1 and set isVisible to true
        if (canvasGroup.alpha >= 1f)
        {
            canvasGroup.alpha = 1f;
            isVisible = true;
        }
    }

    // Fade out all child elements of the PauseMenu UI object
    void FadeOut()
    {
        // Decrease the alpha value of the CanvasGroup
        canvasGroup.alpha -= fadeSpeed * Time.unscaledDeltaTime;

        // Clamp the alpha value at 0 and set isVisible to false
        if (canvasGroup.alpha <= 0f)
        {
            canvasGroup.alpha = 0f;
            isVisible = false;
        }
    }
}