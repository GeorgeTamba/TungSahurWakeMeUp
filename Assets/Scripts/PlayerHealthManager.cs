using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;
    private int currentHealth;
    private bool isInvincible = false;

    public float invincibilityDuration = 2f;

    [Header("UI Settings")]
    public Sprite heartFull;
    public Sprite heartEmpty;
    public Image[] heartImages;

    [Header("Visuals")]
    public SpriteRenderer playerRenderer;
    private Animator animator;

    [Header("Respawn Settings")]
    private Vector3 checkpointPosition;
    private Rigidbody2D rb;
    private bool isRespawning = false;
    private bool isDead = false;

    private float lastXDirection = 1f;

    [Header("Audio")]
    public AudioClip deathSound;
    public AudioClip hitSound; 
    public AudioSource bgmSource;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        checkpointPosition = transform.position;

        SpriteRenderer capsuleRenderer = GetComponent<SpriteRenderer>();
        if (capsuleRenderer != null)
            capsuleRenderer.enabled = false;

        animator = GetComponentInChildren<Animator>();
        UpdateHeartsUI();
    }

    public void TakeDamage()
    {
        if (isInvincible || currentHealth <= 0 || isDead) return;

        if (Mathf.Abs(rb.velocity.x) > 0.01f)
            lastXDirection = Mathf.Sign(rb.velocity.x);

        currentHealth--;
        UpdateHeartsUI();

        if (hitSound != null)
            AudioSource.PlayClipAtPoint(hitSound, transform.position);

        if (currentHealth <= 0)
            StartCoroutine(DeathAnimationAndRestart());
        else
            StartCoroutine(InvincibilityFlash());
    }


    public void FallToDeath()
    {
        if (isRespawning || isDead) return;

        if (Mathf.Abs(rb.velocity.x) > 0.01f)
            lastXDirection = Mathf.Sign(rb.velocity.x);

        currentHealth--;
        UpdateHeartsUI();

        if (currentHealth <= 0)
            StartCoroutine(DeathAnimationAndRestart());
        else
            StartCoroutine(RespawnAtCheckpoint());
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        checkpointPosition = newCheckpoint;
    }

    private IEnumerator InvincibilityFlash()
    {
        isInvincible = true;
        float flashSpeed = 0.1f;

        for (float t = 0; t < invincibilityDuration; t += flashSpeed * 2)
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

        GetComponent<PlayerMovement>().enabled = false;
        transform.position = checkpointPosition;

        if (animator != null)
        {
            animator.SetBool("IsJumping", false);
            animator.SetFloat("Speed", 0f);
            animator.Play("Idle");
        }

        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        StartCoroutine(InvincibilityFlash());

        yield return new WaitForSeconds(1f);

        rb.isKinematic = false;
        GetComponent<PlayerMovement>().enabled = true;
        isRespawning = false;
    }

    private IEnumerator DeathAnimationAndRestart()
    {
        isDead = true;

        foreach (var col in GetComponentsInChildren<Collider2D>())
            col.enabled = false;

        PlayerMovement movement = GetComponent<PlayerMovement>();
        if (movement != null) movement.enabled = false;

        if (animator != null)
            animator.Play("Player_Dead");

        yield return new WaitForSeconds(0.15f);
        if (animator != null) animator.enabled = false;

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1.5f;
        rb.freezeRotation = false;

        rb.velocity = new Vector2(0, 5f); 
        yield return new WaitForSeconds(0.25f);

        rb.velocity = new Vector2(lastXDirection * 2f, -3f); 
        rb.AddTorque(200f); 

        if (bgmSource != null) bgmSource.Stop();

        if (deathSound != null)
            AudioSource.PlayClipAtPoint(deathSound, transform.position);

        yield return new WaitForSeconds(5f); 

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].sprite = i < currentHealth ? heartFull : heartEmpty;
        }
    }
}
