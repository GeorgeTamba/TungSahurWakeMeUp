using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompDetector : MonoBehaviour
{
    public float bounceForce = 12f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Call the enemy's Die() function
            EnemyDeath deathScript = other.GetComponent<EnemyDeath>();
            if (deathScript != null)
            {
                deathScript.Die();
            }

            // Bounce the player up
            Rigidbody2D playerRb = GetComponentInParent<Rigidbody2D>();
            playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce);
        }
    }

}
