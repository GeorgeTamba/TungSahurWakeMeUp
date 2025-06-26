using System.Collections;
using UnityEngine;

public class StompDetector : MonoBehaviour
{
    public float bounceForce = 12f;
    public AudioClip stompSound; 
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 0f; 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyDeath deathScript = other.GetComponent<EnemyDeath>();
            if (deathScript != null)
            {
                deathScript.Die();
            }

            if (stompSound != null)
            {
                audioSource.PlayOneShot(stompSound);
            }

            Rigidbody2D playerRb = GetComponentInParent<Rigidbody2D>();
            playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce);
        }
    }
}
