using UnityEngine;
using System.Collections;

public class PlayerSpawnAudio : MonoBehaviour
{
    public AudioClip spawnClip;
    public float volume = 1f; // bisa diatur besar kecilnya

    void Start()
    {
        AudioListener.pause = false;
        StartCoroutine(PlaySpawnSound());
    }

    IEnumerator PlaySpawnSound()
    {
        yield return null; // tunggu 1 frame
        if (spawnClip != null)
        {
            AudioSource.PlayClipAtPoint(spawnClip, transform.position, volume);
        }
    }
}
