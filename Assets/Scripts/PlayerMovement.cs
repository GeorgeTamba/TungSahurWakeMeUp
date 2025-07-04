using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public bool isControlEnabled = true;
    [Range(0f, 1f)]
    public float jumpSoundVolume = 0.3f;

    public float moveSpeed = 8f;
    public float jumpForce = 16f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Animator animator;

    private Rigidbody2D rb;
    private bool wasGrounded;
    private bool isGrounded;
    private Vector3 originalScale;

    public AudioClip jumpSound;
    public AudioClip[] footstepSounds;
    public AudioSource audioSource;
    public float minPitch = 0.95f;
    public float maxPitch = 1.05f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale; // Save original scale for flipping
    }

    void Update()
    {
        if (!isControlEnabled)
            return;

        // Horizontal Movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        // Ground Check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        // Check landing
        if (!wasGrounded && isGrounded)
        {
            OnLanding();
        }
        // Flip character based on direction
        if (moveInput > 0)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);  // Face right
        else if (moveInput < 0)
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z); // Face left

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetBool("IsJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            // Play jump sound with temporary volume adjustment
            if (jumpSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(jumpSound, jumpSoundVolume);
            }

        }
        wasGrounded = isGrounded; // Save current grounded state for next frame
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Trap"))
        {
            GetComponent<PlayerHealthManager>().TakeDamage(); 
        }
    }

    public void PlayFootstepSound()
    {
        if (footstepSounds.Length == 0 || audioSource == null)
            return;

        // Pick a random footstep clip
        AudioClip clip = footstepSounds[Random.Range(0, footstepSounds.Length)];

        // Randomize pitch
        audioSource.pitch = Random.Range(minPitch, maxPitch);

        audioSource.PlayOneShot(clip);
    }
    public void StartAutoRun()
    {
        StartCoroutine(AutoRunToRight());
    }

    private IEnumerator AutoRunToRight()
    {
        while (true)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(moveSpeed));

            animator.SetBool("IsJumping", false);

            yield return null;
        }
    }
}
