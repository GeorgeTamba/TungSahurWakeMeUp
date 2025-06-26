using UnityEngine;
using System.Collections;

public class PlayerSpawnAudio : MonoBehaviour
{
    public AudioClip spawnClip;
    public float volume = 1f; 

    void Start()
    {
        AudioListener.pause = false;
        StartCoroutine(PlaySpawnSound());
    }

    IEnumerator PlaySpawnSound()
    {
        yield return null; 
        if (spawnClip != null)
        {
            AudioSource.PlayClipAtPoint(spawnClip, transform.position, volume);
        }
    }
}
