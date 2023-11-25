using System.Collections;
using UnityEngine;

public class FallinkTowerHolder : MonoBehaviour
{
    private int baseCount = 0;
    public void IncrementBaseCount()
    {
        baseCount++;
    }
    public void DecrementBaseCount()
    {
        baseCount--;
        if (baseCount <= 0)
        {
            //fallinkTowerMessenger.bools[transform.GetSiblingIndex()] = true;
            PrimitiveMessenger.bools[transform.parent.name + transform.GetSiblingIndex()] = true;
        }
    }
}
