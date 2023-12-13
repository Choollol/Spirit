using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSign : MonoBehaviour
{
    private static float fadeTime = 0.3f;

    private TextMeshPro text;

    private List<int> startIndices = new List<int>();
    private List<int> endIndices = new List<int>();

    private List<string> controlNames = new List<string>();

    private string originalText;
    private void Awake()
    {
        text = transform.GetChild(0).GetComponent<TextMeshPro>();
        text.SetAlpha(0);

        originalText = text.text;
    }
    private void OnEnable()
    {
        EventMessenger.StartListening("ControlsUpdated", UpdateControls);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening("ControlsUpdated", UpdateControls);
    }
    private void UpdateControls()
    {
        text.text = originalText;
        text.text = Util.ReplaceControls(text.text, controlNames);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.activeInHierarchy)
        {
            StopAllCoroutines();
            StartCoroutine(Util.FadeAlpha(text, 1, fadeTime));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.activeInHierarchy)
        {
            StopAllCoroutines();
            StartCoroutine(Util.FadeAlpha(text, 0, fadeTime));
        }
    }
}
