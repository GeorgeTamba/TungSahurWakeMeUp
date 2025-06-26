using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthManager playerHealth = other.GetComponent<PlayerHealthManager>();
            if (playerHealth != null && spawnPoint != null)
            {
                playerHealth.SetCheckpoint(spawnPoint.position);
            }
        }
    }
}
