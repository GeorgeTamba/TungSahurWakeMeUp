using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float jumpForce = 16f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Animator animator;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private bool wasGrounded;

    void Update()
    {
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
            transform.localScale = new Vector3(1.807516f, 1.807516f, 1.807516f);  // Menghadap kanan
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1.807516f, 1.807516f, 1.807516f);  // Menghadap kiri

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetBool("IsJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        wasGrounded = isGrounded; // Simpan status grounded untuk pengecekan di frame berikutnya
    }

    public void OnLanding ()
    {
        animator.SetBool("IsJumping", false);
    }
    // Draw ground check gizmo
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
