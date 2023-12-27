using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaeSpirit : PuzzleComponent
{
    private static float startOpacity = 0.3f;

    private SpriteRenderer spriteRenderer;

    private static float moveSpeed = 1;

    private Vector3 targetPos;
    private float initialDistance;

    ContactFilter2D contactFilter = new ContactFilter2D();
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        targetPos = transform.parent.GetChild(1).position;
        initialDistance = Vector2.Distance(targetPos, transform.position);

        contactFilter.useTriggers = false;

        SetOpacity();
    }
    protected override void SetComplete()
    {
        base.SetComplete();

        transform.position = targetPos;
        SetOpacity();
    }
    public override void ResetPuzzle()
    {
        StopAllCoroutines();
        SetOpacity();
    }
    public bool Move(float pathLength, Vector3 direction)
    {
        RaycastHit2D[] arr = new RaycastHit2D[1];
        if (Physics2D.Raycast(transform.position, direction, contactFilter, arr, pathLength) > 0)
        {
            AudioPlayer.PlaySound("Fae Guide Blocked Sound");
            return false;
        }
        StartCoroutine(HandleMove(pathLength, direction));
        return true;
    }
    private IEnumerator HandleMove(float pathLength, Vector3 direction)
    {
        AudioPlayer.PlaySound("Fae Guide Move Sound", 0.9f, 1.1f);
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
        spriteRenderer.SetAlpha(startOpacity + Mathf.InverseLerp(initialDistance, 0, Vector2.Distance(transform.position, targetPos)) * (1 - startOpacity));
    }
}
