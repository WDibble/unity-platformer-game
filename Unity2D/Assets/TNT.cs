using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private GameObject TNT_Impact;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "CameraConfiner")
        {
            Instantiate(TNT_Impact, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
