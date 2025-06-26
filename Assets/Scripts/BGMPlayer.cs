using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMPlayer : MonoBehaviour
{
    public AudioClip bgmClip;
    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
        source.loop = true;
    }

    void Start()
    {
        if (bgmClip != null)
        {
            source.clip = bgmClip;
            source.Play();
        }
    }

    public void StopBGM()
    {
        if (source.isPlaying)
            source.Stop();
    }

    public void PauseBGM()
    {
        if (source.isPlaying)
            source.Pause();
    }

    public void ResumeBGM()
    {
        if (!source.isPlaying)
            source.UnPause();
    }
}
