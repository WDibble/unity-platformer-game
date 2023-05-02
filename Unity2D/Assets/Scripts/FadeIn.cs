/*
 * FadeIn.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * These scripts are used for fading out and fading in game objects with SpriteRenderer components.
 * The FadeOut script gradually decreases the transparency of the object until it becomes fully transparent and then deactivates it.
 * It is mainly used on the background layers to change depending on the environment.
 */

using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour
{
    [SerializeField] private float fadeInTime;
    [SerializeField] private SpriteRenderer spriteRender;

    // When the object is enabled, start the fade-in process
    private void OnEnable()
    {
        StartCoroutine(ObjectFadeIn());
    }

    // Set the initial transparency of the object to 0
    private void Start()
    {
        spriteRender.color = new Color(1f, 1f, 1f, 0f);
    }

    // Coroutine to gradually increase the object's transparency until it's fully opaque
    private IEnumerator ObjectFadeIn()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeInTime;
            spriteRender.color = new Color(1f, 1f, 1f, t);
            yield return null;
        }
    }
}
