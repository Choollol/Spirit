using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingPylon : PuzzleComponent
{
    // Messenger floats[0]: 1 = moving, 0 = not moving

    private static float riseSpeed = 1;

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

        risenYPos = transform.localPosition.y + 0.5f;
        descendedYPos = transform.localPosition.y - 0.32f;

        if (linkOffset > 0)
        {
            numeralLocalPos = transform.GetChild(0).localPosition;
        }

        isMeleeInteractable = true;

        ResetPuzzle();
    }
    protected override void ResetMessenger()
    {
        base.ResetMessenger();
        PrimitiveMessenger.bools[controllerTransform.name + transform.GetSiblingIndex()] = isRisen;
    }
    protected override void SetComplete()
    {
        base.SetComplete();

        transform.SetLocalPosY(risenYPos);
    }
    public override void ResetPuzzle()
    {
        isRisen = doStartRisen;
        if (doStartRisen)
        {
            transform.SetLocalPosY(risenYPos);
        }
        else
        {
            transform.SetLocalPosY(descendedYPos);
        }
        if (linkOffset > 0)
        {
            transform.GetChild(0).localPosition = numeralLocalPos;
        }
        //risingPylonMessenger.bools[transform.GetSiblingIndex()] = isRisen;
        //risingPylonMessenger.floats[0] = 0;
        PrimitiveMessenger.bools[controllerTransform.name + transform.GetSiblingIndex()] = isRisen;
        StopAllCoroutines();
    }
    public override void MeleeInteract()
    {
        base.MeleeInteract();

        if (PrimitiveMessenger.floats[controllerTransform.name] == 1 || isCompleted)//risingPylonMessenger.floats[0] == 1)
        {
            return;
        }

        if (linkOffset != 0)
        {
            link.Toggle();
        }
        Toggle();

        EventMessenger.TriggerEvent("Check" + transform.parent.name);
    }
    public void Toggle()
    {
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
        //risingPylonMessenger.bools[transform.GetSiblingIndex()] = isRisen;
        PrimitiveMessenger.bools[controllerTransform.name + transform.GetSiblingIndex()] = isRisen;
        AudioPlayer.PlaySound("Rising Pylon Sound", 0.9f, 1.1f);
    }
    private IEnumerator Rise()
    {
        //risingPylonMessenger.floats[0] = 1;
        PrimitiveMessenger.floats[controllerTransform.name] = 1;
        while (transform.localPosition.y < risenYPos)
        {
            transform.localPosition += new Vector3(0, riseSpeed * Time.deltaTime);
            yield return null;
        }
        if (transform.localPosition.y > risenYPos)
        {
            transform.SetLocalPosY(risenYPos);
        }
        //risingPylonMessenger.floats[0] = 0;
        PrimitiveMessenger.floats[controllerTransform.name] = 0;
        yield break;
    }
    private IEnumerator Descend()
    {
        //risingPylonMessenger.floats[0] = 1;
        PrimitiveMessenger.floats[controllerTransform.name] = 1;
        while (transform.localPosition.y > descendedYPos)
        {
            transform.localPosition -= new Vector3(0, riseSpeed * Time.deltaTime);
            yield return null;
        }
        if (transform.localPosition.y < descendedYPos)
        {
            transform.SetLocalPosY(descendedYPos);
        }
        //risingPylonMessenger.floats[0] = 0;
        PrimitiveMessenger.floats[controllerTransform.name] = 0;
        yield break;
    }
}
