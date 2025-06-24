using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D col;

    public float deathDelay = 0.5f;  // Time before destroying the enemy

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    public void Die()
    {
        // Trigger death animation
        animator.SetBool("isDead", true);

        // Disable movement and collision
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }

        if (col != null)
            col.enabled = false;

        // Destroy after delay
        Destroy(gameObject, deathDelay);
    }
}
