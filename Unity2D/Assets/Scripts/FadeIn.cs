using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour
{
    [SerializeField] private float fadeInTime;
    [SerializeField] private SpriteRenderer spriteRender;

    private void OnEnable()
    {
        StartCoroutine(ObjectFadeIn());
    }

    private void Start()
    {
        spriteRender.color = new Color(1f, 1f, 1f, 0f);
    }

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
