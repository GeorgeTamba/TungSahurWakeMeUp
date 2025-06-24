using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private bool isInvincible = false;

    public float invincibilityDuration = 2f;
    public Sprite heartFull;
    public Sprite heartEmpty;
    public Image[] heartImages; // Harus diisi 3 UI Image di Inspector

    public SpriteRenderer playerRenderer;

    private Vector3 checkpointPosition;
    private Rigidbody2D rb;
    private bool isRespawning = false;
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        checkpointPosition = transform.position; // Awal jadi default checkpoint

        // Matikan SpriteRenderer di capsule player
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
        UpdateHeartsUI();

        if (currentHealth <= 0)
        {
            // Mati, ulang scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            // Mulai invincibility
            StartCoroutine(InvincibilityFlash());
        }
    }

    private IEnumerator InvincibilityFlash()
    {
        isInvincible = true;
        float flashSpeed = 0.1f;

        for (float i = 0; i < invincibilityDuration; i += flashSpeed * 2)
        {
            playerRenderer.enabled = false; // Hanya Character
            yield return new WaitForSeconds(flashSpeed);
            playerRenderer.enabled = true;
            yield return new WaitForSeconds(flashSpeed);
        }

        isInvincible = false;
    }


    private void SetAllSpriteRenderers(bool state)
    {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in renderers)
        {
            sr.enabled = state;
        }
    }
    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        checkpointPosition = newCheckpoint;
    }

    public void FallToDeath()
    {
        if (isRespawning) return;

        currentHealth--;
        UpdateHeartsUI();

        if (currentHealth <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            StartCoroutine(RespawnAtCheckpoint());
        }
    }

    private IEnumerator RespawnAtCheckpoint()
    {
        isRespawning = true;

        // Matikan movement
        GetComponent<PlayerMovement>().enabled = false;

        // Teleport ke checkpoint
        transform.position = checkpointPosition;

        // Reset animator
        if (animator != null)
        {
            animator.SetBool("IsJumping", false);
            animator.SetFloat("Speed", 0f);
            animator.Play("Idle");
        }

        // Freeze physics
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        // MULAI invincibility langsung (blinking)
        StartCoroutine(InvincibilityFlash());

        // Tetap freeze selama 1 detik
        yield return new WaitForSeconds(1f);

        // Unfreeze
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
}
