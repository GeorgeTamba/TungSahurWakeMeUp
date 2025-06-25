using System.Collections;
using UnityEngine;

public class StompDetector : MonoBehaviour
{
    public float bounceForce = 12f;
    public AudioClip stompSound; //  Tambahkan ini
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Tambahkan AudioSource jika belum ada
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 0f; // 2D sound
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Hancurkan musuh
            EnemyDeath deathScript = other.GetComponent<EnemyDeath>();
            if (deathScript != null)
            {
                deathScript.Die();
            }

            // Mainkan suara stomp
            if (stompSound != null)
            {
                audioSource.PlayOneShot(stompSound);
            }

            // Bounce player
            Rigidbody2D playerRb = GetComponentInParent<Rigidbody2D>();
            playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce);
        }
    }
}
