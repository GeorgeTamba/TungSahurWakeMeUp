using UnityEngine;
using System.Collections;

public class HouseLightTrigger : MonoBehaviour
{
    public Sprite houseLightOn;
    public GameObject panelToShow;

    private SpriteRenderer spriteRenderer;
    private bool hasTriggered = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (panelToShow != null)
            panelToShow.SetActive(false); // Ensure panel is hidden at start
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;

            // Step 1: Instantly hide the player
            other.gameObject.SetActive(false);

            // Start the sequence of events
            StartCoroutine(Sequence());
        }
    }

    private IEnumerator Sequence()
    {
        // Step 2: Wait 0.5 second, then turn on house light
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.sprite = houseLightOn;

        // Step 3: Wait another 1 second, then show panel
        yield return new WaitForSeconds(1f);
        if (panelToShow != null)
            panelToShow.SetActive(true);
    }
}
