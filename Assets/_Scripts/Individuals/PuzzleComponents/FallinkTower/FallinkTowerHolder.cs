using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallinkTowerHolder : MonoBehaviour
{
    public static float spacing = 0.2f;

    [SerializeField] private GameObject basePrefab;
    [SerializeField] private GameObject topPrefab;
    [SerializeField] private int startingBaseCount;
    private int baseCount = 0;
    private void Awake()
    {
        transform.GetChild(0).SetLocalPosY(startingBaseCount * spacing + spacing / 2);
        float yPos = spacing / 2;
        for (int i = 0; i < startingBaseCount; i++)
        {
            GameObject towerBase = Instantiate(basePrefab, transform);
            towerBase.transform.SetLocalPosY(yPos);
            yPos += spacing;
        }
    }
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
