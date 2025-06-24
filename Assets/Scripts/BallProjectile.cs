using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallProjectile : MonoBehaviour
{
    public float speed = 5f;
    public float lifetime = 3f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Destroy after a while
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void SetDirection(bool isFacingLeft)
    {
        if (isFacingLeft)
            rb.velocity = Vector2.left * speed;
        else
            rb.velocity = Vector2.right * speed;
    }
}
