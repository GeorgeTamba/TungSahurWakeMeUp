using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public string nextSceneName = "NextLevel";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Access GameManager counts
            GameManager gm = FindObjectOfType<GameManager>();

            if (gm != null)
            {
                // Check if player has enough collectibles
                if (gm.bedugCount >= 5 && gm.kentonganCount >= 5 && gm.rebanaCount >= 5)
                {
                    // Trigger fade and scene load
                    FindObjectOfType<FadeTransition>().FadeAndLoadScene(nextSceneName);
                }
                else
                {
                    Debug.Log("You need at least 5 Bedug, 5 Kentongan, and 5 Rebana to finish!");
                    // Optional: Show a UI warning message here
                }
            }
        }
    }
}
