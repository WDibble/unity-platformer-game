using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour
{
    [SerializeField] private float fadeOutTime;
    [SerializeField] private SpriteRenderer spriteRender;

    private void Start()
    {
        spriteRender.color = new Color(1f, 1f, 1f, 1f);
    }

    public void StartFadeOut()
    {
        StartCoroutine(ObjectFadeOut());
    }

    private IEnumerator ObjectFadeOut()
    {
        float t = 1f;
        while (t > 0f)
        {
            t -= Time.deltaTime / fadeOutTime;
            spriteRender.color = new Color(1f, 1f, 1f, t);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}