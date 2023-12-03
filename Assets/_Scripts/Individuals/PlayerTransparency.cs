using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransparency : MonoBehaviour
{
    private static float transparentOpacity = 0.5f;
    private static float fadeTime = 0.3f;

    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            StartCoroutine(Util.FadeAlpha(spriteRenderer, transparentOpacity, fadeTime));
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
            StartCoroutine(Util.FadeAlpha(spriteRenderer, 1, fadeTime));
        }
    }
}
