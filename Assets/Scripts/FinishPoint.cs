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
            GameManager gm = FindObjectOfType<GameManager>();

            if (gm != null)
            {
                if (gm.bedugCount >= 5 && gm.kentonganCount >= 5 && gm.rebanaCount >= 5)
                {
                    // Disable player control
                    PlayerMovement playerMove = other.GetComponent<PlayerMovement>();
                    if (playerMove != null)
                    {
                        playerMove.isControlEnabled = false;
                        playerMove.StartAutoRun();
                    }

                    // Trigger fade and scene load
                    FindObjectOfType<FadeTransition>().FadeAndLoadScene(nextSceneName);
                }
                else
                {
                    Debug.Log("You need at least 5 Bedug, 5 Kentongan, and 5 Rebana to finish!");
                }
            }
        }
    }

}
