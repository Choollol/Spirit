using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallinkTowerBase : PuzzleComponent
{
    private FallinkTowerHolder tower;

    private int towerSiblingIndex;
    public override void Awake()
    {
        base.Awake();

        tower = transform.parent.GetComponent<FallinkTowerHolder>();

        towerSiblingIndex = tower.transform.GetSiblingIndex();

        isRangedInteractable = true;
    }
    public override void OnEnable()
    {
        base.OnEnable();

        tower.IncrementBaseCount();
    }
    public override void OnDisable()
    {
        base.OnDisable();

        tower.DecrementBaseCount();
    }

    public override void RangedInteract()
    {
        base.RangedInteract();

        if (towerSiblingIndex == 0)
        {
            DisableBase(towerSiblingIndex + 1);
        }
        else if (towerSiblingIndex == tower.transform.parent.childCount - 1)
        {
            DisableBase(towerSiblingIndex - 1);
        }
        else
        {
            DisableBase(towerSiblingIndex - 1);
            DisableBase(towerSiblingIndex + 1);
        }
        gameObject.SetActive(false);
        AudioPlayer.PlaySound("Fallink Tower Break Sound", 0.9f, 1.1f, false);
    }
    private void DisableBase(int index)
    {
        if (controllerTransform.childCount <= index)
        {
            return;
        }
        Transform tower = controllerTransform.GetChild(index);
        for (int i = 1; i < tower.childCount; i++)
        {
            if (tower.GetChild(i).gameObject.activeSelf)
            {
                tower.GetChild(i).gameObject.SetActive(false);
                break;
            }
        }
    }
}
