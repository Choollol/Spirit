using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class FallinkTowerHolder : MonoBehaviour
{
    [SerializeField] private ScriptablePrimitive fallinkTowerMessenger;

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
            fallinkTowerMessenger.bools[transform.GetSiblingIndex()] = true;
        }
    }
}
