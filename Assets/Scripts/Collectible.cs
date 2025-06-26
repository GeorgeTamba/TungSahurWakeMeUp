using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string itemName;
    public int itemValue = 1;
    public AudioClip collectSound;

    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Collider2D col;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AddItem(itemName, itemValue);

            if (spriteRenderer != null)
                spriteRenderer.enabled = false;
            if (col != null)
                col.enabled = false;

            if (collectSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(collectSound);
                Destroy(gameObject, collectSound.length);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
