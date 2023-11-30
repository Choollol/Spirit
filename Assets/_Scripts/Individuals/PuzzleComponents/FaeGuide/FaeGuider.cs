using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaeGuider : PuzzleComponent
{
    // floats[name]: 1 = guider in motion, 0 = not

    private static float moveSpeed = 1;

    [SerializeField] private bool doStartRightEnd;
    private bool isOnRightEnd; // Top for vertical

    private bool isVertical;

    private FaeSpirit faeSpirit;
    private float pathLength;
    private float leftX;
    private void Start()
    {
        faeSpirit = controllerTransform.GetChild(0).GetComponent<FaeSpirit>();
        pathLength = transform.parent.GetChild(0).localScale.x / 100;

        isVertical = transform.parent.rotation.eulerAngles.z % 180 != 0;

        isMeleeInteractable = true;

        ResetPuzzle();
    }
    public override void ResetPuzzle()
    {
        base.ResetPuzzle();

        if (doStartRightEnd)
        {
            transform.SetLocalPosX(pathLength / 2);
            leftX = transform.localPosition.x - pathLength;
        }
        else
        {
            transform.SetLocalPosX(-pathLength / 2);
            leftX = transform.localPosition.x;
        }
        isOnRightEnd = doStartRightEnd;
        PrimitiveMessenger.floats[controllerTransform.name] = 0;
        StopAllCoroutines();
    }
    public override void MeleeInteract()
    {
        base.MeleeInteract();

        if (PrimitiveMessenger.floats[controllerTransform.name] == 1 || isCompleted)
        {
            return;
        }

        Toggle();
    }
    private void Toggle()
    {
        if (isOnRightEnd)
        {
            if (isVertical)
            {
                if (faeSpirit.Move(pathLength, -Vector2.up))
                {
                    StartCoroutine(MoveLeft());
                }
            }
            else
            {
                if (faeSpirit.Move(pathLength, -Vector2.right))
                {
                    StartCoroutine(MoveLeft());
                }
            }
        }
        else
        {
            if (isVertical)
            {
                if (faeSpirit.Move(pathLength, Vector2.up))
                {
                    StartCoroutine(MoveRight());
                }
            }
            else
            {
                if (faeSpirit.Move(pathLength, Vector2.right))
                {
                    StartCoroutine(MoveRight());
                }
            }
        }
    }
    private IEnumerator MoveRight()
    {
        PrimitiveMessenger.floats[controllerTransform.name] = 1;
        while (transform.localPosition.x < leftX + pathLength)
        {
            transform.localPosition += new Vector3(moveSpeed * Time.deltaTime, 0);
            yield return null;
        }
        isOnRightEnd = true;
        PrimitiveMessenger.floats[controllerTransform.name] = 0;
        yield break;
    }
    private IEnumerator MoveLeft()
    {
        PrimitiveMessenger.floats[controllerTransform.name] = 1;
        while (transform.localPosition.x > leftX)
        {
            transform.localPosition -= new Vector3(moveSpeed * Time.deltaTime, 0);
            yield return null;
        }
        isOnRightEnd = false;
        PrimitiveMessenger.floats[controllerTransform.name] = 0;
        yield break;
    }
}
