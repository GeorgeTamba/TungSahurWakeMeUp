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
            // Find FadeTransition in the scene and trigger fade
            FindObjectOfType<FadeTransition>().FadeAndLoadScene(nextSceneName);
        }
    }
}
