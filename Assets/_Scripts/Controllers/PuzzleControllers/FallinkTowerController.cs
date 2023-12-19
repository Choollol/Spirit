using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallinkTowerController : PuzzleController
{
    // Messenger bools = does each tower have no bases

    //private static float spacing = 0.2f;

    /*public override void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject towerTop = Instantiate(topPrefab, transform.GetChild(i));
            towerTop.transform.SetLocalPosY(baseCounts[i] * spacing + spacing / 2);
            float yPos = spacing / 2;
            for (int j = 0; j < baseCounts[i]; j++)
            {
                GameObject towerBase = Instantiate(basePrefab, transform.GetChild(i));
                towerBase.transform.SetLocalPosY(yPos);
                yPos += spacing;
            }
        }
        base.Start();
    }
    public override void SetComplete()
    {
        base.SetComplete();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(0).SetLocalPosY(spacing / 2);
            for (int j = 1; j < baseCounts[i] + 1; j++)
            {
                transform.GetChild(i).GetChild(j).gameObject.SetActive(false);
            }
        }
    }*/
}
