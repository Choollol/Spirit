using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectUtil : MonoBehaviour
{
    private ScrollRect scrollRect;
    private float oldY;

    [SerializeField] private float maxScrollAmount;
    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();

        oldY = 1;
    }
    public void LimitScrollAmount()
    {
        float diff = scrollRect.verticalNormalizedPosition - oldY;
        if (diff < 0 && diff < -maxScrollAmount)
        {
            scrollRect.verticalNormalizedPosition = oldY - maxScrollAmount;
        }
        else if (diff > 0 && diff > maxScrollAmount)
        {
            scrollRect.verticalNormalizedPosition = oldY + maxScrollAmount;
        }
        oldY = scrollRect.verticalNormalizedPosition;
    }
}
