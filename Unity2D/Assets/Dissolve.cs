using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    Material material;

    bool isDissolving = false;
    float fade = 1f;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isDissolving = true;
        }

        else if (Input.GetKeyDown(KeyCode.G))
        {
            isDissolving = false;
        }

        if (isDissolving)
        {
            fade -= Time.deltaTime;

            if (fade <= 0f)
            {
                fade = 0f;
            }

            material.SetFloat("_Fade", fade);
        }

        else if (!isDissolving)
        {
            fade += Time.deltaTime;

            if (fade >= 1f)
            {
                fade = 1f;
            }

            material.SetFloat("_Fade", fade);
        }
    }

    // Public method to access the isDissolving field
    public bool PlayerIsDissolving()
    {
        return isDissolving;
    }
}