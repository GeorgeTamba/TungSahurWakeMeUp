using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;
    private int currentHealth;
    private bool isInvincible = false;

    [Header("Invincibility Settings")]
    public float invincibilityDuration = 2f;

    [Header("UI Settings")]
    public Sprite heartFull;
    public Sprite heartEmpty;
    public Image[] heartImages; // Harus diisi 3 UI Image di Inspector

    [Header("Sprite Renderer")]
    public SpriteRenderer playerRenderer;

    [Header("Audio")]
    public AudioClip hitSound;
    private AudioSource audioSource;

    private Vector3 checkpointPosition;
    private Rigidbody2D rb;
    private bool isRespawning = false;
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        checkpointPosition = transform.position; // Default checkpoint di posisi awal
        audioSource = GetComponent<AudioSource>();

        // Matikan SpriteRenderer capsule player
        SpriteRenderer selfRenderer = GetComponent<SpriteRenderer>();
        if (selfRenderer != null)
            selfRenderer.enabled = false;

        animator = GetComponentInChildren<Animator>();
        UpdateHeartsUI();
    }

    public void TakeDamage()
    {
        if (isInvincible || currentHealth <= 0)
            return;

        currentHealth--;

        // Mainkan suara terkena hit
        if (hitSound != null && audioSource != null)
            audioSource.PlayOneShot(hitSound);

        UpdateHeartsUI();

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            StartCoroutine(InvincibilityFlash());
        }
    }

    public void FallToDeath()
    {
        if (isRespawning) return;

        currentHealth--;

        // Mainkan suara terkena hit (jatuh)
        if (hitSound != null && audioSource != null)
            audioSource.PlayOneShot(hitSound);

        UpdateHeartsUI();

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            StartCoroutine(RespawnAtCheckpoint());
        }
    }

    private IEnumerator InvincibilityFlash()
    {
        isInvincible = true;
        float flashSpeed = 0.1f;

        for (float i = 0; i < invincibilityDuration; i += flashSpeed * 2)
        {
            playerRenderer.enabled = false;
            yield return new WaitForSeconds(flashSpeed);
            playerRenderer.enabled = true;
            yield return new WaitForSeconds(flashSpeed);
        }

        isInvincible = false;
    }

    private IEnumerator RespawnAtCheckpoint()
    {
        isRespawning = true;

        // Nonaktifkan movement
        GetComponent<PlayerMovement>().enabled = false;

        // Pindahkan ke checkpoint
        transform.position = checkpointPosition;

        // Reset animasi
        if (animator != null)
        {
            animator.SetBool("IsJumping", false);
            animator.SetFloat("Speed", 0f);
            animator.Play("Idle");
        }

        // Freeze physics
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        // Mulai blinking seketika
        StartCoroutine(InvincibilityFlash());

        // Tunggu 1 detik di tempat
        yield return new WaitForSeconds(1f);

        // Unfreeze dan aktifkan movement
        rb.isKinematic = false;
        GetComponent<PlayerMovement>().enabled = true;

        isRespawning = false;
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentHealth)
                heartImages[i].sprite = heartFull;
            else
                heartImages[i].sprite = heartEmpty;
        }
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        checkpointPosition = newCheckpoint;
    }

    private void SetAllSpriteRenderers(bool state)
    {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in renderers)
        {
            sr.enabled = state;
        }
    }
}
