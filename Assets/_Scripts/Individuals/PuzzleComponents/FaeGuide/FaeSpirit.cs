using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaeSpirit : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private static float moveSpeed = 1;

    private Vector3 targetPos;
    private float initialDistance;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        targetPos = transform.parent.GetChild(1).position;
        initialDistance = Vector2.Distance(targetPos, transform.position);

        SetOpacity();
    }
    public bool Move(float pathLength, Vector3 direction)
    {
        if (Physics2D.Raycast(transform.position, direction, pathLength))
        {
            AudioPlayer.PlaySound("Fae Guide Blocked Sound");
            return false;
        }
        StartCoroutine(HandleMove(pathLength, direction));
        return true;
    }
    private IEnumerator HandleMove(float pathLength, Vector3 direction)
    {
        AudioPlayer.PlaySound("Fae Guide Move Sound");
        float distanceTraveled = 0;
        while (distanceTraveled < pathLength)
        {
            transform.position += direction * moveSpeed * Time.deltaTime;
            distanceTraveled += moveSpeed * Time.deltaTime;
            SetOpacity();
            yield return null;
        }
        if (Vector2.Distance(transform.position, targetPos) < 0.02)
        {
            PrimitiveMessenger.bools[transform.parent.name + 0] = true;
            EventMessenger.TriggerEvent("Check" + transform.parent.name);
        }
        yield break;
    }
    private void SetOpacity()
    {
        spriteRenderer.SetAlpha(initialDistance / Vector2.Distance(transform.position, targetPos) - 0.7f);
    }
}
