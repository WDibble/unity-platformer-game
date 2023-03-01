using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;

    [SerializeField] private AudioSource deathSound;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
            deathSound.Play();
        }
    }

    private void Die()
    {
        body.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }

    private void LevelRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}