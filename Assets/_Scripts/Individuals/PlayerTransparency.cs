using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerTransparency : MonoBehaviour
{
    private static float transparentOpacity = 0.5f;
    private static float fadeTime = 0.3f;

    private SpriteRenderer spriteRenderer;
    private Tilemap tilemap;
    private void Start()
    {
        if (spriteRenderer = GetComponent<SpriteRenderer>()) { }
        else if (tilemap = GetComponent<Tilemap>()) { }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        StopAllCoroutines();
        if (collision.gameObject.CompareTag("Player"))
        {
            if (spriteRenderer)
            {
                StartCoroutine(Util.FadeAlpha(spriteRenderer, transparentOpacity, fadeTime));
            }
            else if (tilemap)
            {
                StartCoroutine(Util.FadeAlpha(tilemap, transparentOpacity, fadeTime));
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        StopAllCoroutines();
        if (collision.gameObject.CompareTag("Player"))
        {
            if (spriteRenderer)
            {
                StartCoroutine(Util.FadeAlpha(spriteRenderer, 1, fadeTime));
            }
            else if (tilemap)
            {
                StartCoroutine(Util.FadeAlpha(tilemap, 1, fadeTime));
            }
        }
    }
}
