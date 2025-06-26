using UnityEngine;
using System.Collections;

public class ArrowBlink : MonoBehaviour
{
    public Sprite terangSprite;  // Arrow On
    public Sprite gelapSprite;   // Arrow Off
    public float blinkInterval = 0.5f; // Blink Interval

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(BlinkArrow());
    }

    IEnumerator BlinkArrow()
    {
        while (true)
        {
            spriteRenderer.sprite = terangSprite;
            yield return new WaitForSeconds(blinkInterval);

            spriteRenderer.sprite = gelapSprite;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
