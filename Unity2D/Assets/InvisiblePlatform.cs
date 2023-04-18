using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisiblePlatform : MonoBehaviour
{
    Material material;

    public bool alreadyInvisible;
    private bool isDissolving = false;
    private float fade = 1f;

    private Collider2D platformCollider;
    private bool collisionEnabled = true;
    private bool collisionDisabled = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get the material and collider components of the platform
        material = GetComponent<SpriteRenderer>().material;
        platformCollider = GetComponent<Collider2D>();

        // Set the platform to be invisible and disable its collider if alreadyInvisible is true
        if (alreadyInvisible)
        {
            fade = 0f;
            material.SetFloat("_Fade", fade);
            platformCollider.enabled = false;
            collisionEnabled = false;
            collisionDisabled = true;

            if (CompareTag("Trap"))
            {
                tag = "Untagged";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player's invisibility state is different from the platform's initial state
        if (PlayerVisibility.GetInvisible() != alreadyInvisible)
        {
            if (!isDissolving)
            {
                isDissolving = true;
            }
        }
        else
        {
            if (isDissolving)
            {
                isDissolving = false;
            }
        }

        // Handle platform turning invisible
        if (isDissolving)
        {
            fade -= Time.deltaTime;

            if (fade <= 0f)
            {
                fade = 0f;
            }

            // Disable the platform's collider and remove the "Trap" tag as soon as it starts turning invisible
            if (!collisionDisabled)
            {
                platformCollider.enabled = false;
                collisionEnabled = false;
                collisionDisabled = true;

                if (CompareTag("Trap"))
                {
                    tag = "Untagged";
                }
            }

            material.SetFloat("_Fade", fade);
        }
        // Handle platform turning visible
        else if (!isDissolving)
        {
            fade += Time.deltaTime;

            if (fade >= 1f)
            {
                fade = 1f;
            }

            // Enable the platform's collider and add the "Trap" tag as soon as it starts turning visible
            if (!collisionEnabled)
            {
                platformCollider.enabled = true;
                collisionEnabled = true;
                collisionDisabled = false;

                if (gameObject.name.Contains("Spikes"))
                {
                    tag = "Trap";
                }

                if (gameObject.name.Contains("Blade"))
                {
                    tag = "Trap";
                }
            }

            material.SetFloat("_Fade", fade);
        }
    }
}
