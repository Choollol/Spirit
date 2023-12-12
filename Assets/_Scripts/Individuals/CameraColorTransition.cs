using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColorTransition : MonoBehaviour
{
    private static float distanceFromCamera = float.MaxValue;
    private static CameraColorTransition closest;

    [SerializeField] private Color leftColor;
    [SerializeField] private Color rightColor;

    private Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;

        if (Vector2.Distance(transform.position, mainCamera.transform.position) < distanceFromCamera)
        {
            distanceFromCamera = Vector2.Distance(transform.position, mainCamera.transform.position);
            closest = this;
        }
    }
    private void LateUpdate()
    {
        if (closest == this)
        {
            if (mainCamera.transform.position.x < transform.position.x)
            {
                mainCamera.backgroundColor = leftColor;
            }
            else
            {
                mainCamera.backgroundColor = rightColor;
            }
            closest = null;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.position.x < transform.position.x)
            {
                StopAllCoroutines();
                StartCoroutine(Util.FadeColor(mainCamera, leftColor, 0.5f));
            }
            else if (collision.transform.position.x > transform.position.x)
            {
                StopAllCoroutines();
                StartCoroutine(Util.FadeColor(mainCamera, rightColor, 0.5f));
            }
        }
    }
}
