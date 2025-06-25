using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrower : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform ballSpawnPoint;

    public AudioClip throwSound;
    private AudioSource audioSource;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void ThrowBall()
    {
        GameObject ball = Instantiate(projectilePrefab, ballSpawnPoint.position, ballSpawnPoint.rotation);

        BallProjectile proj = ball.GetComponent<BallProjectile>();

        // Detect enemy facing direction
        bool isFacingLeft = !GetComponent<SpriteRenderer>().flipX;

        // Set ball direction based on enemy facing
        proj.SetDirection(isFacingLeft);

        // Play throw sound exactly at the moment ball spawns
        if (throwSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(throwSound);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("isAttacking", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("isAttacking", false);
        }
    }
}
