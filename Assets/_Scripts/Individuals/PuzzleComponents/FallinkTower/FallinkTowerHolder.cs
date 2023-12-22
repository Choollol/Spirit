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

    private int siblingIndex;
    private void Awake()
    {
        siblingIndex = transform.GetSiblingIndex();

        transform.GetChild(0).SetLocalPosY(startingBaseCount * spacing + spacing / 2);
        float yPos = spacing / 2;
        for (int i = 0; i < startingBaseCount; i++)
        {
            GameObject towerBase = Instantiate(basePrefab, transform);
            towerBase.transform.SetLocalPosY(yPos);
            yPos += spacing;
        }

        // Whether holder at index has no bases
        PrimitiveMessenger.bools[transform.parent.name + siblingIndex] = false;
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
            PrimitiveMessenger.bools[transform.parent.name + siblingIndex] = true;
        }
        if (transform.parent.childCount == 1)
        {
            return;
        }
    }
    public void ResetPuzzle()
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        StartCoroutine(HandleReset());
    }
    private IEnumerator HandleReset()
    {
        yield return null;
        EventMessenger.TriggerEvent("Reset");
    }
}
