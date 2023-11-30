using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    private static float fadeSpeed = 1;

    private Image image;
    private void OnEnable()
    {
        EventMessenger.StartListening("StartTransition", StartTransition);
        EventMessenger.StartListening("EndTransition", EndTransition);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening("StartTransition", StartTransition);
        EventMessenger.StopListening("EndTransition", EndTransition);
    }
    private void Start()
    {
        image = GetComponent<Image>();
    }
    private void StartTransition()
    {
        StartCoroutine(HandleStartTransition());
    }
    private IEnumerator HandleStartTransition()
    {
        PrimitiveMessenger.bools["isTransitionFading"] = true;
        while (image.color.a < 1)
        {
            image.color += new Color(0, 0, 0, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        PrimitiveMessenger.bools["isTransitionFading"] = false;
        yield break;
    }
    private void EndTransition()
    {
        StartCoroutine(HandleEndTransition());
    }
    private IEnumerator HandleEndTransition()
    {
        PrimitiveMessenger.bools["isTransitionFading"] = true;
        while (image.color.a > 0)
        {
            image.color -= new Color(0, 0, 0, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        PrimitiveMessenger.bools["isTransitionFading"] = false;
        yield break;
    }
}
