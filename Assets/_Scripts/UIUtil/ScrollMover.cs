using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollMover : MonoBehaviour
{
    private RectTransform left;
    private RectTransform right;
    [SerializeField] private RectTransform mask;
    [SerializeField] private float size;
    [SerializeField] private float speed;

    private bool doMove;
    void Start()
    {
        left = transform.GetChild(0).GetComponent<RectTransform>();
        right = transform.GetChild(1).GetComponent<RectTransform>();

        left.SetPosX(-left.sizeDelta.x / 2);
        right.SetPosX(right.sizeDelta.x / 2);

        doMove = true;

        mask.sizeDelta = new Vector2(0, mask.sizeDelta.y);
    }

    void Update()
    {
        if (doMove)
        {
            if (mask.sizeDelta.x < size)
            {
                mask.sizeDelta = new Vector2(mask.sizeDelta.x + speed, mask.sizeDelta.y);
                left.localPosition -= new Vector3(speed / 2, 0);
                right.localPosition += new Vector3(speed / 2, 0);
            }
            else if (left.localPosition.x < -size / 2 || right.localPosition.x > size / 2)
            {
                left.SetPosX(-size / 2);
                right.SetPosX(size / 2);
                doMove = false;
            }
        }
    }
}
