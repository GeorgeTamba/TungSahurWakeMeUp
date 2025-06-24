using UnityEngine;
using System.Collections;
public class HouseLightTrigger : MonoBehaviour
{
    public Sprite houseLightOn;
    [SerializeField] private GameObject panelToShow;

    private SpriteRenderer spriteRenderer;
    private bool hasTriggered = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (panelToShow != null)
            panelToShow.SetActive(false); // Hide panel at start, even if it's inactive in editor
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
            panelToShow.SetActive(true); // Show the panel even if it's inactive at start
    }
}
