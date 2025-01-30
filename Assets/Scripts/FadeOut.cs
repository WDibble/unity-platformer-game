/*
 * FadeOut.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script is used to gradually fade out a game object with a sprite renderer component over a certain amount of time.
 * The script takes in a fade-out time value and a sprite renderer component as serialized fields.
 * It is mainly used on the backgrounds to adapt with the enviroment textures.
 */

using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour
{
    [SerializeField] private float fadeOutTime;
    [SerializeField] private SpriteRenderer spriteRender;

    // Set the initial transparency of the object to 1 (fully opaque)
    private void Start()
    {
        spriteRender.color = new Color(1f, 1f, 1f, 1f);
    }

    // Start the fade-out process
    public void StartFadeOut()
    {
        StartCoroutine(ObjectFadeOut());
    }

    // Coroutine to gradually decrease the object's transparency until it's fully transparent
    private IEnumerator ObjectFadeOut()
    {
        float t = 1f;
        while (t > 0f)
        {
            t -= Time.deltaTime / fadeOutTime;
            spriteRender.color = new Color(1f, 1f, 1f, t);
            yield return null;
        }
        // Deactivate the game object after it has become fully transparent
        gameObject.SetActive(false);
    }
}