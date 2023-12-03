using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransparency : MonoBehaviour
{
    private static float transparentOpacity = 0.5f;
    private static float fadeSpeed = 2f;

    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private IEnumerator FadeOpaque()
    {
        while (spriteRenderer.color.a < 1)
        {
            spriteRenderer.AddAlpha(fadeSpeed * Time.deltaTime);
            yield return null;
        }
        yield break;
    }
    private IEnumerator FadeTransparent()
    {
        while (spriteRenderer.color.a > transparentOpacity)
        {
            spriteRenderer.AddAlpha(-fadeSpeed * Time.deltaTime);
            yield return null;
        }
        yield break;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        StopAllCoroutines();
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FadeTransparent());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        StopAllCoroutines();
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FadeOpaque());
        }
    }
}
