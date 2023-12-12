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
    private void OnEnable()
    {
        EventMessenger.StartListening("ControlsUpdated", UpdateControls);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening("ControlsUpdated", UpdateControls);
    }
    void Start()
    {
        text = transform.GetChild(0).GetComponent<TextMeshPro>();
        text.SetAlpha(0);

        for (int i = 0; i < text.text.Length; i++)
        {
            if (text.text[i] == '[')
            {
                startIndices.Add(i);
            }
            else if (text.text[i] == ']')
            {
                startIndices.Add(i);
            }
        }
        UpdateControls();
    }
    private void UpdateControls()
    {
        for (int i = 0; i < startIndices.Count; i++)
        {
            text.text = text.text[0..startIndices.Count] +
                InputManager.Controls[text.text[(startIndices[i] + 1)..endIndices[i]]][0] +
                text.text[(endIndices[i] + 1)..text.text.Length];
        }
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
