using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float patrolDistance = 3f;
    public float patrolSpeed = 3f;
    public bool startMovingLeft = true;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool movingLeft;
    private float leftBoundary;
    private float rightBoundary;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set patrol boundaries based on starting position
        float startX = transform.position.x;
        leftBoundary = startX - patrolDistance;
        rightBoundary = startX + patrolDistance;

        // Set starting direction
        movingLeft = startMovingLeft;
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        float moveDir = movingLeft ? -1f : 1f;
        rb.velocity = new Vector2(moveDir * patrolSpeed, rb.velocity.y);

        spriteRenderer.flipX = !movingLeft;

        // Turn around at patrol boundaries with small tolerance
        if (movingLeft && transform.position.x <= leftBoundary + 0.05f)
        {
            movingLeft = false;
        }
        else if (!movingLeft && transform.position.x >= rightBoundary - 0.05f)
        {
            movingLeft = true;
        }
    }

}
