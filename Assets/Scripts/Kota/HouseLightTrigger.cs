using UnityEngine;
using System.Collections;

public class HouseLightTrigger : MonoBehaviour
{
    public Sprite houseLightOn;
    [SerializeField] private GameObject panelToShow;

    [Header("Audio")]
    [SerializeField] private AudioClip finishSound;
    private AudioSource audioSource;

    [Header("Background Music")]
    public BGMPlayer bgmPlayer; // Drag dari scene

    private SpriteRenderer spriteRenderer;
    private bool hasTriggered = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (panelToShow != null)
            panelToShow.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            other.gameObject.SetActive(false);
            StartCoroutine(Sequence());
        }
    }

    private IEnumerator Sequence()
    {
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = houseLightOn;

        yield return new WaitForSeconds(1f);

        if (panelToShow != null)
        {
            panelToShow.SetActive(true);

            // Hentikan BGM
            if (bgmPlayer != null)
                bgmPlayer.StopBGM();

            // Mainkan suara panel finish
            if (finishSound != null)
                audioSource.PlayOneShot(finishSound);
        }
    }
}
