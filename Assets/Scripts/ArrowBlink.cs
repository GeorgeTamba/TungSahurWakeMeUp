using UnityEngine;
using System.Collections;

public class ArrowBlink : MonoBehaviour
{
    public Sprite terangSprite;  // Gambar nyala
    public Sprite gelapSprite;   // Gambar mati
    public float blinkInterval = 0.5f; // Detik antara kedipan

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
