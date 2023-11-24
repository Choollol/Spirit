using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingPylon : PuzzleComponent
{
    // Messenger floats[0]: 1 = moving, 0 = not moving

    private static float riseSpeed = 1;

    [SerializeField] private ScriptablePrimitive risingPylonMessenger;

    [SerializeField] private int linkOffset;
    [SerializeField] private bool doStartRisen;

    private bool isRisen;
    private RisingPylon link;

    private float risenYPos;
    private float descendedYPos;

    private Vector3 numeralLocalPos;
    void Start()
    {
        link = controllerTransform.GetChild((transform.GetSiblingIndex() + linkOffset) % controllerTransform.childCount).GetComponent<RisingPylon>();

        risenYPos = transform.position.y + 0.5f;
        descendedYPos = transform.position.y - 0.32f;

        if (linkOffset > 0)
        {
            numeralLocalPos = transform.GetChild(0).localPosition;
        }

        ResetPuzzle();
    }
    public override void ResetPuzzle()
    {
        isRisen = doStartRisen;
        if (doStartRisen)
        {
            transform.SetPosY(risenYPos);
        }
        else
        {
            transform.SetPosY(descendedYPos);
        }
        if (linkOffset > 0)
        {
            transform.GetChild(0).localPosition = numeralLocalPos;
        }
        risingPylonMessenger.bools[transform.GetSiblingIndex()] = isRisen;
        StopAllCoroutines();
        risingPylonMessenger.floats[0] = 0;
    }
    public override void MeleeInteract()
    {
        if (isCompleted)
        {
            return;
        }
        Toggle();
    }
    public void Toggle()
    {
        if (risingPylonMessenger.floats[0] == 1)
        {
            return;
        }
        if (linkOffset != 0)
        {
            link.Toggle();
        }
        if (isRisen)
        {
            isRisen = false;
            StartCoroutine(Descend());
        }
        else
        {
            isRisen = true;
            StartCoroutine(Rise());
        }
        risingPylonMessenger.bools[transform.GetSiblingIndex()] = isRisen;
    }
    private IEnumerator Rise()
    {
        risingPylonMessenger.floats[0] = 1;
        while (transform.position.y < risenYPos)
        {
            transform.position += new Vector3(0, riseSpeed * Time.deltaTime);
            yield return null;
        }
        if (transform.position.y > risenYPos)
        {
            transform.SetPosY(risenYPos);
        }
        risingPylonMessenger.floats[0] = 0;
        EventMessenger.TriggerEvent("Check" + transform.parent.name);
        yield break;
    }
    private IEnumerator Descend()
    {
        risingPylonMessenger.floats[0] = 1;
        while (transform.position.y > descendedYPos)
        {
            transform.position -= new Vector3(0, riseSpeed * Time.deltaTime);
            yield return null;
        }
        if (transform.position.y < descendedYPos)
        {
            transform.SetPosY(descendedYPos);
        }
        risingPylonMessenger.floats[0] = 0;
        yield break;
    }
}
